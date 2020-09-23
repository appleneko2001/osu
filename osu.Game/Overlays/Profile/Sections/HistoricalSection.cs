﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Game.Online.API.Requests;
using osu.Game.Overlays.Profile.Sections.Historical;
using osu.Game.Overlays.Profile.Sections.Ranks;

namespace osu.Game.Overlays.Profile.Sections
{
    public class HistoricalSection : ProfileSection
    {
        public override string Title => "歷史記錄";

        public override string Identifier => "historical";

        public HistoricalSection()
        {
            Children = new Drawable[]
            {
                new PaginatedMostPlayedBeatmapContainer(User),
                new PaginatedScoreContainer(ScoreType.Recent, User, "最近遊玩 (24小時內)", CounterVisibilityState.VisibleWhenZero),
            };
        }
    }
}
