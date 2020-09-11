﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Screens.Play.PlayerSettings
{
    public class VisualSettings : PlayerSettingsGroup
    {
        private readonly PlayerSliderBar<double> dimSliderBar;
        private readonly PlayerSliderBar<double> blurSliderBar;
        private readonly PlayerCheckbox showStoryboardToggle;
        private readonly PlayerCheckbox beatmapSkinsToggle;
        private readonly PlayerCheckbox beatmapHitsoundsToggle;

        public VisualSettings()
            : base("Visual Settings")
        {
            Children = new Drawable[]
            {
                new OsuSpriteText
                {
                    Text = "背景暗度:"
                },
                dimSliderBar = new PlayerSliderBar<double>
                {
                    DisplayAsPercentage = true
                },
                new OsuSpriteText
                {
                    Text = "背景模糊:"
                },
                blurSliderBar = new PlayerSliderBar<double>
                {
                    DisplayAsPercentage = true
                },
                new OsuSpriteText
                {
                    Text = "切換:"
                },
                showStoryboardToggle = new PlayerCheckbox { LabelText = "故事板 / 影片" },
                beatmapSkinsToggle = new PlayerCheckbox { LabelText = "使用圖譜的皮膚" },
                beatmapHitsoundsToggle = new PlayerCheckbox { LabelText = "使用圖譜的打擊音" }
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            dimSliderBar.Bindable = config.GetBindable<double>(OsuSetting.DimLevel);
            blurSliderBar.Bindable = config.GetBindable<double>(OsuSetting.BlurLevel);
            showStoryboardToggle.Current = config.GetBindable<bool>(OsuSetting.ShowStoryboard);
            beatmapSkinsToggle.Current = config.GetBindable<bool>(OsuSetting.BeatmapSkins);
            beatmapHitsoundsToggle.Current = config.GetBindable<bool>(OsuSetting.BeatmapHitsounds);
        }
    }
}
