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
        public override string Name => "Wind Up";  // [RequestImprove] ��Wind Down�O�@�ӱ��p �����D���½Ķ����n 
        public override string Acronym => "WU";
        public override string Description => "�A/�p���W�`����?";
        public override IconUsage? Icon => FontAwesome.Solid.ChevronCircleUp;
        public override double ScoreMultiplier => 1.0;

        [SettingSource("�򥻳t��", "���ت��_�l�t��")]
        public override BindableNumber<double> InitialRate { get; } = new BindableDouble
        {
            MinValue = 0.5,
            MaxValue = 1,
            Default = 1,
            Value = 1,
            Precision = 0.01,
        };

        [SettingSource("�̤j�t��", "�̤j�t�ת�����")]
        public override BindableNumber<double> FinalRate { get; } = new BindableDouble
        {
            MinValue = 1.01,
            MaxValue = 2,
            Default = 1.5,
            Value = 1.5,
            Precision = 0.01,
        };

        [SettingSource("�վ㭵��", "�����O�_��t�פ@�_�ܤ�")]
        public override BindableBool AdjustPitch { get; } = new BindableBool
        {
            Default = true,
            Value = true
        };

        public override Type[] IncompatibleMods => base.IncompatibleMods.Append(typeof(ModWindDown)).ToArray();
    }
}
