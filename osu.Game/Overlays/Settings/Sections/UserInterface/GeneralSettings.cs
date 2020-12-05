﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Graphics.UserInterface;

namespace osu.Game.Overlays.Settings.Sections.UserInterface
{
    public class GeneralSettings : SettingsSubsection
    {
        protected override string Header => "基本設定";

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "拖動時旋轉指針",
                    Current = config.GetBindable<bool>(OsuSetting.CursorRotation)
                },
                new SettingsSlider<float, SizeSlider>
                {
                    LabelText = "主選單光標大小",
                    Current = config.GetBindable<float>(OsuSetting.MenuCursorSize),
                    KeyboardStep = 0.01f
                },
                new SettingsCheckbox
                {
                    LabelText = "視差效果",
                    Current = config.GetBindable<bool>(OsuSetting.MenuParallax)
                },
                new SettingsSlider<float, TimeSlider>
                {
                    LabelText = "按住確定操作用時",
                    Current = config.GetBindable<float>(OsuSetting.UIHoldActivationDelay),
                    KeyboardStep = 50
                },
            };
        }

        private class TimeSlider : OsuSliderBar<float>
        {
            public override string TooltipText => Current.Value.ToString("N0") + "ms";
        }
    }
}
