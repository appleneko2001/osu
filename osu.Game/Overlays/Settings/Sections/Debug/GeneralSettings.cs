﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Game.Screens.Import;

namespace osu.Game.Overlays.Settings.Sections.Debug
{
    public class GeneralSettings : SettingsSubsection
    {
        protected override string Header => "一般設定";

        [BackgroundDependencyLoader(true)]
        private void load(FrameworkDebugConfigManager config, FrameworkConfigManager frameworkConfig, OsuGame game)
        {
            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "顯示日誌圖層",
                    Current = frameworkConfig.GetBindable<bool>(FrameworkSetting.ShowLogOverlay)
                },
                new SettingsCheckbox
                {
                    LabelText = "不使用\"前到後\"渲染設定",
                    Current = config.GetBindable<bool>(DebugSetting.BypassFrontToBackPass)
                }
            };
            Add(new SettingsButton
            {
                Text = "匯入檔案...",
                Action = () => game?.PerformFromScreen(menu => menu.Push(new FileImportScreen()))
            });
        }
    }
}
