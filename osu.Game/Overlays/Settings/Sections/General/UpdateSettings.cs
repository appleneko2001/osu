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
            if (updateManager?.CanCheckForUpdate == true)
            {
                Add(new SettingsCheckbox
                {
                    LabelText = "使用中文化更新流",
                    TooltipText = "使用來自 appleneko2001/osu 的更新流來更新遊戲, 將保留中文化界面. 關閉後將使用ppy官方更新流.",
                    Current = config.GetBindable<bool>(OsuSetting.UseTranslationUpdateRepo)
                });
            }

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
                                    Text = $"You are running the latest release ({game.Version})",
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
