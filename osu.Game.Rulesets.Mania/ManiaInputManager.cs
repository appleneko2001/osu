// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Mania
{
    public class ManiaInputManager : RulesetInputManager<ManiaAction>
    {
        public ManiaInputManager(RulesetInfo ruleset, int variant)
            : base(ruleset, variant, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum ManiaAction
    {
        [Description("特殊鍵 1")]
        Special1 = 1,

        [Description("特殊鍵 2")]
        Special2,

        // This offsets the start value of normal keys in-case we add more special keys
        // above at a later time, without breaking replays/configs.

        [Description("按鍵 1")]
        Key1 = 10,

        [Description("按鍵 2")]
        Key2,

        [Description("按鍵 3")]
        Key3,

        [Description("按鍵 4")]
        Key4,

        [Description("按鍵 5")]
        Key5,

        [Description("按鍵 6")]
        Key6,

        [Description("按鍵 7")]
        Key7,

        [Description("按鍵 8")]
        Key8,

        [Description("按鍵 9")]
        Key9,

        [Description("按鍵 10")]
        Key10,

        [Description("按鍵 11")]
        Key11,

        [Description("按鍵 12")]
        Key12,

        [Description("按鍵 13")]
        Key13,

        [Description("按鍵 14")]
        Key14,

        [Description("按鍵 15")]
        Key15,

        [Description("按鍵 16")]
        Key16,

        [Description("按鍵 17")]
        Key17,

        [Description("按鍵 18")]
        Key18,

        [Description("按鍵 19")]
        Key19,

        [Description("按鍵 20")]
        Key20
    }
}
