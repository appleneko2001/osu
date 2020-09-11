// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using osu.Game.Rulesets.UI.Scrolling;

namespace osu.Game.Rulesets.Mania.UI
{
    public enum ManiaScrollingDirection
    {
        [Description("向上")]
        Up = ScrollingDirection.Up,
        [Description("向下")]
        Down = ScrollingDirection.Down
    }
}
