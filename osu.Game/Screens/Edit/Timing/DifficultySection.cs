﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Beatmaps.ControlPoints;

namespace osu.Game.Screens.Edit.Timing
{
    internal class DifficultySection : Section<DifficultyControlPoint>
    {
        private SliderWithTextBoxInput<double> multiplierSlider;

        [BackgroundDependencyLoader]
        private void load()
        {
            Flow.AddRange(new[]
            {
                multiplierSlider = new SliderWithTextBoxInput<double>("Speed Multiplier")
                {
                    LabelText = "速度乘積",
                    Bindable = new DifficultyControlPoint().SpeedMultiplierBindable,
                    RelativeSizeAxes = Axes.X,
                }
            });
        }

        protected override void OnControlPointChanged(ValueChangedEvent<DifficultyControlPoint> point)
        {
            if (point.NewValue != null)
            {
                multiplierSlider.Current = point.NewValue.SpeedMultiplierBindable;
            }
        }

        protected override DifficultyControlPoint CreatePoint()
        {
            var reference = Beatmap.Value.Beatmap.ControlPointInfo.DifficultyPointAt(SelectedGroup.Value.Time);

            return new DifficultyControlPoint
            {
                SpeedMultiplier = reference.SpeedMultiplier,
            };
        }
    }
}
