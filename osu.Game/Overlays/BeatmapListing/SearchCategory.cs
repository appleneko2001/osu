// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Overlays.BeatmapListing
{
    public enum SearchCategory
    {
        [Description("所有")]
        Any, 
        [Description("擁有排行榜")]
        Leaderboard,
        [Description("已進榜")]
        Ranked,
        Qualified,
        Loved,
        [Description("收藏")]
        Favourites,

        [Description("待處理&製作中")]
        Pending,
        [Description("拋棄")]
        Graveyard, 
        [Description("我的圖譜")]
        Mine,
    }
}
