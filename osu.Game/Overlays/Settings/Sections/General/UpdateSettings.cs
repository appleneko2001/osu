// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Threading.Tasks;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osu.Game.Configuration;
using osu.Game.Overlays.Notifications;
using osu.Game.Overlays.Settings.Sections.Maintenance;
using osu.Game.Updater;

namespace osu.Game.Overlays.Settings.Sections.General
{
    public class UpdateSettings : SettingsSubsection
    {
        [Resolved(CanBeNull = true)]
        private UpdateManager updateManager { get; set; }

        protected override string Header => "更新設定";

        private SettingsButton checkForUpdatesButton;

        [Resolved(CanBeNull = true)]
        private NotificationOverlay notifications { get; set; }

        [BackgroundDependencyLoader(true)]
        private void load(Storage storage, OsuConfigManager config, OsuGame game)
        {
            Add(new SettingsEnumDropdown<ReleaseStream>
            {
                LabelText = "發行版本",
                Current = config.GetBindable<ReleaseStream>(OsuSetting.ReleaseStream),
            });

            if (updateManager?.CanCheckForUpdate == true)
            {
                Add(checkForUpdatesButton = new SettingsButton
                {
                    Text = "檢查版本更新",
                    Action = () =>
                    {
                        checkForUpdatesButton.Enabled.Value = false;
                        Task.Run(updateManager.CheckForUpdateAsync).ContinueWith(t => Schedule(() =>
                        {
                            if (!t.Result)
                            {
                                notifications?.Post(new SimpleNotification
                                {
                                    Text = $"當前版本已經到最新! ({game.Version})",
                                    Icon = FontAwesome.Solid.CheckCircle,
                                });
                            }

                            checkForUpdatesButton.Enabled.Value = true;
                        }));
                    }
                });
            }

            if (RuntimeInfo.IsDesktop)
            {
                Add(new SettingsButton
                {
                    Text = "顯示 osu! 檔案夾",
                    Action = storage.OpenInNativeExplorer,
                });

                Add(new SettingsButton
                {
                    Text = "修改目錄位置...",
                    Action = () => game?.PerformFromScreen(menu => menu.Push(new MigrationSelectScreen()))
                });
            }
        }
    }
}
