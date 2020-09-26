// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Beatmaps.Timing
{
    public enum TimeSignatures
    {
        [Description("1/4 一般四拍")]
        SimpleQuadruple = 4,
        [Description("3/4 三拍")]
        SimpleTriple = 3
    }
}
