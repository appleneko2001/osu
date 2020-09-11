// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text. 
using osu.Game.Utils;
using System.ComponentModel;

namespace osu.Game.Overlays.BeatmapListing
{
    [HasOrderedElements]
    public enum SearchLanguage
    {
        [Description("所有")]
        [Order(0)]
        Any,
        [Description("未指定")]
        [Order(14)]
        Unspecified,
        [Description("英語")]
        [Order(1)]
        English,
        [Description("日語")]
        [Order(6)]
        Japanese,
        [Description("漢語")]
        [Order(2)]
        Chinese,
        [Description("樂器演奏")]
        [Order(12)]
        Instrumental,
        [Description("韓語")]
        [Order(7)]
        Korean,
        [Description("法語")]
        [Order(3)]
        French,
        [Description("德語")]
        [Order(4)]
        German,
        [Description("瑞典語")]
        [Order(9)]
        Swedish,
        [Description("西班牙語")]
        [Order(8)]
        Spanish,
        [Description("意大利語")]
        [Order(5)]
        Italian,
        [Description("俄語")]
        [Order(10)]
        Russian,
        [Description("波蘭語")]
        [Order(11)]
        Polish,
        [Description("其他")]
        [Order(13)]
        Other
    }
}
