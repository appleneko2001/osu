// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using osu.Game.Configuration;

namespace osu.Game.Rulesets.Mods
{
    public class ModWindUp : ModTimeRamp
    {
        public override string Name => "Wind Up";  // [RequestImprove] 跟Wind Down是一個情況 不知道怎麼翻譯比較好 
        public override string Acronym => "WU";
        public override string Description => "你能跟上節奏嗎?";
        public override IconUsage? Icon => FontAwesome.Solid.ChevronCircleUp;
        public override double ScoreMultiplier => 1.0;

        [SettingSource("基本速度", "曲目的起始速度")]
        public override BindableNumber<double> InitialRate { get; } = new BindableDouble
        {
            MinValue = 0.5,
            MaxValue = 1,
            Default = 1,
            Value = 1,
            Precision = 0.01,
        };

        [SettingSource("最大速度", "最大速度的限制")]
        public override BindableNumber<double> FinalRate { get; } = new BindableDouble
        {
            MinValue = 1.01,
            MaxValue = 2,
            Default = 1.5,
            Value = 1.5,
            Precision = 0.01,
        };

        [SettingSource("調整音高", "音高是否跟速度一起變化")]
        public override BindableBool AdjustPitch { get; } = new BindableBool
        {
            Default = true,
            Value = true
        };

        public override Type[] IncompatibleMods => base.IncompatibleMods.Append(typeof(ModWindDown)).ToArray();
    }
}
