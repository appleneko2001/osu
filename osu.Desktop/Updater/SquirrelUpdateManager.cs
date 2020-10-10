// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osu.Game;
using osu.Game.Graphics;
using osu.Game.Overlays;
using osu.Game.Overlays.Notifications;
using osuTK;
using osuTK.Graphics;
using Squirrel;
using LogLevel = Splat.LogLevel;

namespace osu.Desktop.Updater
{
    public class SquirrelUpdateManager : osu.Game.Updater.UpdateManager
    {
        private OsuGame game;
        private UpdateManager updateManager;
        private CancellableDownloader cancellableDownloader;
        private NotificationOverlay notificationOverlay;

        public Task PrepareUpdateAsync() => UpdateManager.RestartAppWhenExited();

        private static readonly Logger logger = Logger.GetLogger("updater");

        /// <summary>
        /// Whether an update has been downloaded but not yet applied.
        /// </summary>
        private bool updatePending;

        [BackgroundDependencyLoader]
        private void load(NotificationOverlay notification, OsuGame game)
        {
            this.game = game;
            notificationOverlay = notification;

            Splat.Locator.CurrentMutable.Register(() => new SquirrelLogger(), typeof(Splat.ILogger));
        }

        protected override async Task<bool> PerformUpdateCheck() => await checkForUpdateAsync();

        private async Task<bool> checkForUpdateAsync(bool useDeltaPatching = true, UpdateProgressNotification notification = null)
        {
            string updateRepository = game.UseTranslationRepositoryUpdate ?
                "https://github.com/appleneko2001/osu" :
                "https://github.com/ppy/osu";
            // should we schedule a retry on completion of this check?
            bool scheduleRecheck = true;

            try
            {
                Logger.Log($"Checking updates on repository: {updateRepository}");

                cancellableDownloader ??= new CancellableDownloader();
                updateManager ??= await UpdateManager.GitHubUpdateManager(updateRepository, @"osulazer-zh-tw", null, cancellableDownloader, true);

                var info = await updateManager.CheckForUpdate(!useDeltaPatching);

                if (info.ReleasesToApply.Count == 0)
                {
                    if (updatePending)
                    {
                        // the user may have dismissed the completion notice, so show it again.
                        notificationOverlay.Post(new UpdateCompleteNotification(this));
                        return true;
                    }

                    // no updates available. bail and retry later.
                    return false;
                }

                if (notification == null)
                {
                    notification = new UpdateProgressNotification(this) { State = ProgressNotificationState.Active };
                    Schedule(() => notificationOverlay.Post(notification));
                }

                notification.Progress = 0;
                notification.Text = @"下載更新中...";

                try
                {
                    var cancellation = notification.CancellationToken;
                    cancellableDownloader.SetCancellationToken(cancellation);

                    await updateManager.DownloadReleases(info.ReleasesToApply, p => notification.Progress = p / 100f);

                    cancellation.ThrowIfCancellationRequested();

                    notification.Progress = 0;
                    notification.Text = @"正在安裝...";

                    await updateManager.ApplyReleases(info, p => notification.Progress = p / 100f);

                    notification.State = ProgressNotificationState.Completed;
                    updatePending = true;
                }
                catch (OperationCanceledException)
                {
                    notification.Text = "更新已取消.";
                    notification.State = ProgressNotificationState.Cancelled;
                    Logger.Log($"Update cancelled by user.", LoggingTarget.Runtime, Framework.Logging.LogLevel.Verbose);
                }
                catch (Exception e)
                {
                    if (useDeltaPatching)
                    {
                        logger.Add(@"delta patching failed; will attempt full download!");

                        // could fail if deltas are unavailable for full update path (https://github.com/Squirrel/Squirrel.Windows/issues/959)
                        // try again without deltas.
                        await checkForUpdateAsync(false, notification);
                        scheduleRecheck = false;
                    }
                    else
                    {
                        notification.State = ProgressNotificationState.Cancelled;
                        Logger.Error(e, @"update failed!");
                    }
                }
            }
            catch (Exception)
            {
                // we'll ignore this and retry later. can be triggered by no internet connection or thread abortion.
            }
            finally
            {
                if (scheduleRecheck)
                {
                    // check again in 30 minutes.
                    Scheduler.AddDelayed(async () => await checkForUpdateAsync(), 60000 * 30);
                }
            }

            return true;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            updateManager?.Dispose();
        }

        private class UpdateCompleteNotification : ProgressCompletionNotification
        {
            [Resolved]
            private OsuGame game { get; set; }

            public UpdateCompleteNotification(SquirrelUpdateManager updateManager)
            {
                Text = @"更新已安裝, 點擊重新啟動遊戲!";

                Activated = () =>
                {
                    updateManager.PrepareUpdateAsync()
                                 .ContinueWith(_ => updateManager.Schedule(() => game.GracefullyExit()));
                    return true;
                };
            }
        }

        private class UpdateProgressNotification : ProgressNotification
        {
            private readonly SquirrelUpdateManager updateManager;

            public UpdateProgressNotification(SquirrelUpdateManager updateManager)
            {
                this.updateManager = updateManager;
            }

            protected override Notification CreateCompletionNotification()
            {
                return new UpdateCompleteNotification(updateManager);
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                IconContent.AddRange(new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = ColourInfo.GradientVertical(colours.YellowDark, colours.Yellow)
                    },
                    new SpriteIcon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Icon = FontAwesome.Solid.Upload,
                        Colour = Color4.White,
                        Size = new Vector2(20),
                    }
                });
            }
        }

        private class SquirrelLogger : Splat.ILogger, IDisposable
        {
            public LogLevel Level { get; set; } = LogLevel.Info;

            public void Write(string message, LogLevel logLevel)
            {
                if (logLevel < Level)
                    return;

                logger.Add(message);
            }

            public void Dispose()
            {
            }
        }
    }
}
