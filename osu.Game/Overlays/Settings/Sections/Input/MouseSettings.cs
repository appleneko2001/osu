// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Graphics.UserInterface;
using osu.Game.Input;

namespace osu.Game.Overlays.Settings.Sections.Input
{
    public class MouseSettings : SettingsSubsection
    {
        protected override string Header => "滑鼠設定";

        private readonly BindableBool rawInputToggle = new BindableBool();
        private Bindable<double> sensitivityBindable = new BindableDouble();
        private Bindable<string> ignoredInputHandler;

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager osuConfig, FrameworkConfigManager config)
        {
            var configSensitivity = config.GetBindable<double>(FrameworkSetting.CursorSensitivity);

            // use local bindable to avoid changing enabled state of game host's bindable.
            sensitivityBindable = configSensitivity.GetUnboundCopy();
            configSensitivity.BindValueChanged(val => sensitivityBindable.Value = val.NewValue);
            sensitivityBindable.BindValueChanged(val => configSensitivity.Value = val.NewValue);

            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "原生輸入 (Raw Input)",
                    TooltipText = rawInputToggle.Disabled ? "當前作業系統不支援該特性" : "啟用原生輸入 (Raw Input) 後會忽略 Windows 的滑鼠速度設定, 讓游標更精準地移動.",
                    Current = rawInputToggle
                },
                new SensitivitySetting
                {
                    LabelText = "指針靈敏度",
                    Current = sensitivityBindable
                },
                new SettingsCheckbox
                {
                    LabelText = "映射絕對座標輸入至 osu! 視窗",
                    Current = config.GetBindable<bool>(FrameworkSetting.MapAbsoluteInputToWindow)
                },
                new SettingsEnumDropdown<OsuConfineMouseMode>
                {
                    LabelText = "限制光標到遊戲畫面中",
                    Current = osuConfig.GetBindable<OsuConfineMouseMode>(OsuSetting.ConfineMouseMode)
                },
                new SettingsCheckbox
                {
                    LabelText = "遊戲中禁用滾輪",
                    Current = osuConfig.GetBindable<bool>(OsuSetting.MouseDisableWheel)
                },
                new SettingsCheckbox
                {
                    LabelText = "遊戲中禁用滑鼠按鍵",
                    Current = osuConfig.GetBindable<bool>(OsuSetting.MouseDisableButtons)
                },
            };

            if (RuntimeInfo.OS != RuntimeInfo.Platform.Windows)
            {
                rawInputToggle.Disabled = true;
                sensitivityBindable.Disabled = true;
            }
            else
            {
                rawInputToggle.ValueChanged += enabled =>
                {
                    // this is temporary until we support per-handler settings.
                    const string raw_mouse_handler = @"OsuTKRawMouseHandler";
                    const string standard_mouse_handler = @"OsuTKMouseHandler";

                    ignoredInputHandler.Value = enabled.NewValue ? standard_mouse_handler : raw_mouse_handler;
                };

                ignoredInputHandler = config.GetBindable<string>(FrameworkSetting.IgnoredInputHandlers);
                ignoredInputHandler.ValueChanged += handler =>
                {
                    bool raw = !handler.NewValue.Contains("Raw");
                    rawInputToggle.Value = raw;
                    sensitivityBindable.Disabled = !raw;
                };

                ignoredInputHandler.TriggerChange();
            }
        }

        private class SensitivitySetting : SettingsSlider<double, SensitivitySlider>
        {
            public SensitivitySetting()
            {
                KeyboardStep = 0.01f;
                TransferValueOnCommit = true;
            }
        }

        private class SensitivitySlider : OsuSliderBar<double>
        {
            public override string TooltipText => Current.Disabled ? "開啟原生輸入方可調整該設定" : $"{base.TooltipText}x";
        }
    }
}
