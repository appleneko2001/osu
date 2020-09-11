// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Screens.Select.Filter
{
    public enum SortMode
    {
        [Description("作曲者")]
        Artist,

        [Description("做圖者")]
        Author,

        [Description("每分鐘節拍")]
        BPM,

        [Description("已安裝圖譜日期")]
        DateAdded,

        [Description("難度")]
        Difficulty,

        [Description("長度")]
        Length,

        [Description("Rank Achieved")]
        RankAchieved,

        [Description("標題")]
        Title
    }
}
