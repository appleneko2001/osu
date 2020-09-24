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
using osu.Game.Configuration;
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
        private NotificationOverlay notificationOverlay;

        public Task PrepareUpdateAsync() => UpdateManager.RestartAppWhenExited();

        private static readonly Logger logger = Logger.GetLogger("updater");

        [BackgroundDependencyLoader]
        private void load(NotificationOverlay notification, OsuGame game)
        {
            this.game = game;
            notificationOverlay = notification;
            
            Splat.Locator.CurrentMutable.Register(() => new SquirrelLogger(), typeof(Splat.ILogger));
        }

        protected override async Task PerformUpdateCheck() => await checkForUpdateAsync();

        private async Task checkForUpdateAsync(bool useDeltaPatching = true, UpdateProgressNotification notification = null)
        {
            string updateRepository = game.UseTranslationRepositoryUpdate ?
                "https://github.com/appleneko2001/osu" :
                "https://github.com/ppy/osu";
            // should we schedule a retry on completion of this check?
            bool scheduleRecheck = true;

            try
            {
                updateManager ??= await UpdateManager.GitHubUpdateManager(updateRepository, @"osulazer-zh-tw", null, null, true);

                var info = await updateManager.CheckForUpdate(!useDeltaPatching);
                if (info.ReleasesToApply.Count == 0)
                    // no updates available. bail and retry later.
                    return;

                if (notification == null)
                {
                    notification = new UpdateProgressNotification(this) { State = ProgressNotificationState.Active };
                    Schedule(() => notificationOverlay.Post(notification));
                }

                notification.Progress = 0;
                notification.Text = @"�U����s��...";

                try
                {
                    await updateManager.DownloadReleases(info.ReleasesToApply, p => notification.Progress = p / 100f);

                    notification.Progress = 0;
                    notification.Text = @"���b�w��...";

                    await updateManager.ApplyReleases(info, p => notification.Progress = p / 100f);

                    notification.State = ProgressNotificationState.Completed;
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
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            updateManager?.Dispose();
        }

        private class UpdateProgressNotification : ProgressNotification
        {
            private readonly SquirrelUpdateManager updateManager;
            private OsuGame game;

            public UpdateProgressNotification(SquirrelUpdateManager updateManager)
            {
                this.updateManager = updateManager;
            }

            protected override Notification CreateCompletionNotification()
            {
                return new ProgressCompletionNotification
                {
                    Text = @"��s�w�w��, �I�����s�ҰʹC��!",
                    Activated = () =>
                    {
                        updateManager.PrepareUpdateAsync()
                                     .ContinueWith(_ => updateManager.Schedule(() => game.GracefullyExit()));
                        return true;
                    }
                };
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours, OsuGame game)
            {
                this.game = game;

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
