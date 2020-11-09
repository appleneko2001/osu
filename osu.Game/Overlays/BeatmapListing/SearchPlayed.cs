// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Overlays.BeatmapListing
{
    public enum SearchPlayed
    {
        [Description("所有")]
        Any,

        [Description("玩過的圖譜")]
        Played,

        [Description("未玩過的圖譜")]
        Unplayed
    }
}
