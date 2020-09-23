﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Overlays.Profile.Sections.Ranks;
using osu.Game.Online.API.Requests;

namespace osu.Game.Overlays.Profile.Sections
{
    public class RanksSection : ProfileSection
    {
        public override string Title => "排名";

        public override string Identifier => "top_ranks";

        public RanksSection()
        {
            Children = new[]
            {
                new PaginatedScoreContainer(ScoreType.Best, User, "表現最佳", CounterVisibilityState.AlwaysHidden, "沒有成績記錄. :("),
                new PaginatedScoreContainer(ScoreType.Firsts, User, "排名榜首", CounterVisibilityState.AlwaysVisible)
            };
        }
    }
}
