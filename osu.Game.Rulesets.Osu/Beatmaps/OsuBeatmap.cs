﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Osu.Objects;

namespace osu.Game.Rulesets.Osu.Beatmaps
{
    public class OsuBeatmap : Beatmap<OsuHitObject>
    {
        public override IEnumerable<BeatmapStatistic> GetStatistics()
        {
            int circles = HitObjects.Count(c => c is HitCircle);
            int sliders = HitObjects.Count(s => s is Slider);
            int spinners = HitObjects.Count(s => s is Spinner);

            return new[]
            {
                new BeatmapStatistic
                {
                    Name = @"圓圈數量",
                    Content = circles.ToString(),
                    CreateIcon = () => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Circles),
                },
                new BeatmapStatistic
                {
                    Name = @"滑條數量",
                    Content = sliders.ToString(),
                    CreateIcon = () => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Sliders),
                },
                new BeatmapStatistic
                {
                    Name = @"轉盤數量",
                    Content = spinners.ToString(),
                    CreateIcon = () => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Spinners),
                }
            };
        }
    }
}
