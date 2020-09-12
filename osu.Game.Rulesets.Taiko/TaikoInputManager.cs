// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Taiko
{
    public class TaikoInputManager : RulesetInputManager<TaikoAction>
    {
        public TaikoInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum TaikoAction
    {
        [Description("左 (外)")]
        LeftRim,

        [Description("左 (中)")]
        LeftCentre,

        [Description("右 (中)")]
        RightCentre,

        [Description("右 (外)")]
        RightRim
    }
}
