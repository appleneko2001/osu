﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using osu.Game.Graphics.Sprites;
using osu.Game.Screens.Select.Leaderboards;

namespace osu.Game.Overlays.BeatmapSet.Scores
{
    public class NoScoresPlaceholder : Container
    {
        private readonly SpriteText text;

        public NoScoresPlaceholder()
        {
            AutoSizeAxes = Axes.Both;
            Child = text = new OsuSpriteText();
        }

        public override void Show() => this.FadeIn(200, Easing.OutQuint);

        public override void Hide() => this.FadeOut(200, Easing.OutQuint);

        public void ShowWithScope(BeatmapLeaderboardScope scope)
        {
            Show();

            switch (scope)
            {
                default:
                    text.Text = new LocalisedString(@"No scores have been set yet. Maybe you can be the first!");
                    break;

                case BeatmapLeaderboardScope.Friend:
                    text.Text = new LocalisedString(@"None of your friends have set a score on this map yet.");
                    break;

                case BeatmapLeaderboardScope.Country:
                    text.Text = new LocalisedString(@"No one from your country has set a score on this map yet.");
                    break;
            }
        }
    }
}
