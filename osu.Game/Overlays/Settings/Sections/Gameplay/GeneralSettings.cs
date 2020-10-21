// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Overlays.Settings.Sections.Gameplay
{
    public class GeneralSettings : SettingsSubsection
    {
        protected override string Header => "一般設定";

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsSlider<double>
                {
                    LabelText = "背景暗度",
                    Current = config.GetBindable<double>(OsuSetting.DimLevel),
                    KeyboardStep = 0.01f,
                    DisplayAsPercentage = true
                },
                new SettingsSlider<double>
                {
                    LabelText = "背景模糊",
                    Current = config.GetBindable<double>(OsuSetting.BlurLevel),
                    KeyboardStep = 0.01f,
                    DisplayAsPercentage = true
                },
                new SettingsCheckbox
                {
                    LabelText = "休息片刻時背景變亮",
                    Current = config.GetBindable<bool>(OsuSetting.LightenDuringBreaks)
                },
                new SettingsEnumDropdown<HUDVisibilityMode>
                {
                    LabelText = "遊戲中介面圖層",
                    Current = config.GetBindable<HUDVisibilityMode>(OsuSetting.HUDVisibilityMode)
                },
                new SettingsCheckbox
                {
                    LabelText = "在進度上顯示區域難度",
                    Current = config.GetBindable<bool>(OsuSetting.ShowProgressGraph)
                },
                new SettingsCheckbox
                {
                    LabelText = "強制顯示體力值",
                    Current = config.GetBindable<bool>(OsuSetting.ShowHealthDisplayWhenCantFail),
                    Keywords = new[] { "hp", "bar" }
                },
                new SettingsCheckbox
                {
                    LabelText = "體力值低時遊戲區域漸變到紅",
                    Current = config.GetBindable<bool>(OsuSetting.FadePlayfieldWhenHealthLow),
                },
                new SettingsCheckbox
                {
                    LabelText = "顯示按鍵圖層",
                    Current = config.GetBindable<bool>(OsuSetting.KeyOverlay)
                },
                new SettingsCheckbox
                {
                    LabelText = "立體打擊音效",
                    Current = config.GetBindable<bool>(OsuSetting.PositionalHitSounds)
                },
                new SettingsEnumDropdown<ScoreMeterType>
                {
                    LabelText = "分數計數器類型",
                    Current = config.GetBindable<ScoreMeterType>(OsuSetting.ScoreMeter)
                },
                new SettingsEnumDropdown<ScoringMode>
                {
                    LabelText = "分數顯示模式",
                    Current = config.GetBindable<ScoringMode>(OsuSetting.ScoreDisplayMode),
                    Keywords = new[] { "scoring" }
                },
            };

            if (RuntimeInfo.OS == RuntimeInfo.Platform.Windows)
            {
                Add(new SettingsCheckbox
                {
                    LabelText = "遊戲中禁用Win鍵",
                    Current = config.GetBindable<bool>(OsuSetting.GameplayDisableWinKey)
                });
            }
        }
    }
}
