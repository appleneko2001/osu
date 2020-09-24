// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;

namespace osu.Game.Rulesets.Mods
{
    public abstract class ModNoFail : ModBlockFail
    {
        public override string Name => "�L����";
        public override string Acronym => "NF";
        public override IconUsage? Icon => OsuIcon.ModNofail;
        public override ModType Type => ModType.DifficultyReduction;
        public override string Description => "�S������O��C���L�{���קK \"��M����\" ��n���F";
        public override double ScoreMultiplier => 0.5;
        public override bool Ranked => true;
        public override Type[] IncompatibleMods => new[] { typeof(ModRelax), typeof(ModSuddenDeath), typeof(ModAutoplay) };
    }
}
