// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Beatmaps
{
    public enum BeatmapSetOnlineStatus
    {
        [Description("未知狀態")]
        None = -3,
        [Description("拋棄")]
        Graveyard = -2,
        [Description("製作中")]
        WIP = -1,
        [Description("待處理")]
        Pending = 0,
        [Description("已進榜")]
        Ranked = 1,
        [Description("已批准")]
        Approved = 2,
        [Description("合格")]
        Qualified = 3,
        [Description("喜歡")]
        Loved = 4,
    }
}
