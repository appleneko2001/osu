// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;

namespace osu.Game.Overlays.Settings.Sections.General
{
    public class LanguageSettings : SettingsSubsection
    {
        protected override string Header => "語言設定";

        [BackgroundDependencyLoader]
        private void load(FrameworkConfigManager frameworkConfig)
        {
            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "以原始語言顯示歌曲資訊",
                    Bindable = frameworkConfig.GetBindable<bool>(FrameworkSetting.ShowUnicode),
                    TooltipText = "如歌曲有提供原始語言資訊, 則使用原始語言顯示歌曲資訊."
                },
            };
        }
    }
}
