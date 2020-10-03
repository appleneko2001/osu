// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Overlays.BeatmapListing
{
    public enum SortCriteria
    {
        [Description("標題")]
        Title,
        [Description("演出者")]
        Artist,
        [Description("難度")]
        Difficulty,
        [Description("已進榜")]
        Ranked,
        [Description("評分")]
        Rating,
        [Description("遊玩次數")]
        Plays,
        [Description("喜歡")]
        Favourites,
        Relevance // [RequestImprove]
    }
}
