// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;

namespace osu.Game.Overlays.Settings.Sections.Input
{
    public class KeyboardSettings : SettingsSubsection
    {
        protected override string Header => "鍵盤設定";

        public KeyboardSettings(KeyBindingPanel keyConfig)
        {
            Children = new Drawable[]
            {
                new SettingsButton
                {
                    Text = "按鍵映射",
                    TooltipText = "變動每個按鍵的操作和遊戲中按鍵功能",
                    Action = keyConfig.ToggleVisibility
                },
            };
        }
    }
}
