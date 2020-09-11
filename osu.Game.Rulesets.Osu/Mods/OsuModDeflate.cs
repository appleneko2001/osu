// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using osu.Game.Configuration;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModDeflate : OsuModObjectScaleTween
    {
        public override string Name => "從大變小";

        public override string Acronym => "DF";

        public override IconUsage? Icon => FontAwesome.Solid.CompressArrowsAlt;

        public override string Description => "以正確的圓圈大小打擊!";

        [SettingSource("開始大小", "初始大小乘數將應用於所有物件.")]
        public override BindableNumber<float> StartScale { get; } = new BindableFloat
        {
            MinValue = 1f,
            MaxValue = 25f,
            Default = 2f,
            Value = 2f,
            Precision = 0.1f,
        };
    }
}
