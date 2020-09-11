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
        public override string Name => "Wind Down"; // [RequestImprove] �����D�o�ӭn���½Ķ����n �p�G�����D���B�ͳ·Ыإߤ@��issue�i�D�ڳo�ӭn���½Ķ����n���� :)
        public override string Acronym => "WD";
        public override string Description => "�C �U ��...";
        public override IconUsage? Icon => FontAwesome.Solid.ChevronCircleDown;
        public override double ScoreMultiplier => 1.0;

        [SettingSource("�򥻳t��", "���ت��_�l�t��")]
        public override BindableNumber<double> InitialRate { get; } = new BindableDouble
        {
            MinValue = 1,
            MaxValue = 2,
            Default = 1,
            Value = 1,
            Precision = 0.01,
        };

        [SettingSource("�̧C�t��", "�̧C�t�ת�����")]
        public override BindableNumber<double> FinalRate { get; } = new BindableDouble
        {
            MinValue = 0.5,
            MaxValue = 0.99,
            Default = 0.75,
            Value = 0.75,
            Precision = 0.01,
        };

        [SettingSource("�վ㭵��", "�����O�_��t�פ@�_�ܤ�")]
        public override BindableBool AdjustPitch { get; } = new BindableBool
        {
            Default = true,
            Value = true
        };

        public override Type[] IncompatibleMods => base.IncompatibleMods.Append(typeof(ModWindUp)).ToArray();
    }
}
