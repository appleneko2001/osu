// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Rulesets.UI
{
    public enum PlayfieldBorderStyle
    {
        [Description("關閉")]
        None,
        [Description("邊角")]
        Corners,
        [Description("完全")]
        Full
    }
}
