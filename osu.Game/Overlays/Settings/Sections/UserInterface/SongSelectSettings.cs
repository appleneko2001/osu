﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Graphics.UserInterface;

namespace osu.Game.Overlays.Settings.Sections.UserInterface
{
    public class SongSelectSettings : SettingsSubsection
    {
        private Bindable<double> minStars;
        private Bindable<double> maxStars;

        protected override string Header => "歌曲選擇";

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            minStars = config.GetBindable<double>(OsuSetting.DisplayStarsMinimum);
            maxStars = config.GetBindable<double>(OsuSetting.DisplayStarsMaximum);

            minStars.ValueChanged += min => maxStars.Value = Math.Max(min.NewValue, maxStars.Value);
            maxStars.ValueChanged += max => minStars.Value = Math.Min(max.NewValue, minStars.Value);

            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "設定滑鼠右鍵為絕對滾動",
                    Current = config.GetBindable<bool>(OsuSetting.SongSelectRightMouseScroll),
                },
                new SettingsCheckbox
                {
                    LabelText = "顯示已轉換的圖譜",
                    Current = config.GetBindable<bool>(OsuSetting.ShowConvertedBeatmaps),
                },
                new SettingsSlider<double, StarsSlider>
                {
                    LabelText = "過濾圖譜難度",
                    Current = config.GetBindable<double>(OsuSetting.DisplayStarsMinimum),
                    KeyboardStep = 0.1f,
                    Keywords = new[] { "minimum", "maximum", "star", "difficulty" }
                },
                new SettingsSlider<double, MaximumStarsSlider>
                {
                    LabelText = "到",
                    Current = config.GetBindable<double>(OsuSetting.DisplayStarsMaximum),
                    KeyboardStep = 0.1f,
                    Keywords = new[] { "minimum", "maximum", "star", "difficulty" }
                },
                new SettingsEnumDropdown<RandomSelectAlgorithm>
                {
                    LabelText = "隨機選擇算法",
                    Current = config.GetBindable<RandomSelectAlgorithm>(OsuSetting.RandomSelectAlgorithm),
                }
            };
        }

        private class MaximumStarsSlider : StarsSlider
        {
            public override string TooltipText => Current.IsDefault ? "不限制" : base.TooltipText;
        }

        private class StarsSlider : OsuSliderBar<double>
        {
            public override string TooltipText => Current.Value.ToString(@"0.## stars");
        }
    }
}
