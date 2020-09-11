// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using osu.Game.Configuration;

namespace osu.Game.Rulesets.Mods
{
    public class ModWindDown : ModTimeRamp
    {
        public override string Name => "Wind Down"; // [RequestImprove] 不知道這個要怎麼翻譯比較好 如果有知道的朋友麻煩建立一個issue告訴我這個要怎麼翻譯比較好謝謝 :)
        public override string Acronym => "WD";
        public override string Description => "慢 下 來...";
        public override IconUsage? Icon => FontAwesome.Solid.ChevronCircleDown;
        public override double ScoreMultiplier => 1.0;

        [SettingSource("基本速度", "曲目的起始速度")]
        public override BindableNumber<double> InitialRate { get; } = new BindableDouble
        {
            MinValue = 1,
            MaxValue = 2,
            Default = 1,
            Value = 1,
            Precision = 0.01,
        };

        [SettingSource("最低速度", "最低速度的限制")]
        public override BindableNumber<double> FinalRate { get; } = new BindableDouble
        {
            MinValue = 0.5,
            MaxValue = 0.99,
            Default = 0.75,
            Value = 0.75,
            Precision = 0.01,
        };

        [SettingSource("調整音高", "音高是否跟速度一起變化")]
        public override BindableBool AdjustPitch { get; } = new BindableBool
        {
            Default = true,
            Value = true
        };

        public override Type[] IncompatibleMods => base.IncompatibleMods.Append(typeof(ModWindUp)).ToArray();
    }
}
