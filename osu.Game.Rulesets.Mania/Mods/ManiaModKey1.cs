// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Game.Rulesets.Mania.Mods
{
    public class ManiaModKey1 : ManiaKeyMod
    {
        public override int KeyCount => 1;
        public override string Name => "一鍵";
        public override string Acronym => "1K";
        public override string Description => @"只用一個按鍵玩.";
    }
}
