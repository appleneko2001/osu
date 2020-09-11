// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using osu.Game.Configuration;

namespace osu.Game.Rulesets.Osu.Mods
{
    internal class OsuModGrow : OsuModObjectScaleTween
    {
        public override string Name => "從小變大";

        public override string Acronym => "GR";

        public override IconUsage? Icon => FontAwesome.Solid.ArrowsAltV;

        public override string Description => "以正確的圓圈大小打擊!";

        [SettingSource("開始大小", "初始大小乘數將應用於所有物件.")]
        public override BindableNumber<float> StartScale { get; } = new BindableFloat
        {
            MinValue = 0f,
            MaxValue = 0.99f,
            Default = 0.5f,
            Value = 0.5f,
            Precision = 0.01f,
        };
    }
}
