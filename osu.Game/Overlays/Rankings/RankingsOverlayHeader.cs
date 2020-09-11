// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Bindables;
using osu.Game.Rulesets;
using osu.Game.Users;
using System.ComponentModel;

namespace osu.Game.Overlays.Rankings
{
    public class RankingsOverlayHeader : TabControlOverlayHeader<RankingsScope>
    {
        public Bindable<RulesetInfo> Ruleset => rulesetSelector.Current;

        public Bindable<Country> Country => countryFilter.Current;

        private OverlayRulesetSelector rulesetSelector;
        private CountryFilter countryFilter;

        protected override OverlayTitle CreateTitle() => new RankingsTitle();

        protected override Drawable CreateTitleContent() => rulesetSelector = new OverlayRulesetSelector();

        protected override Drawable CreateContent() => countryFilter = new CountryFilter();

        protected override Drawable CreateBackground() => new OverlayHeaderBackground("Headers/rankings");

        private class RankingsTitle : OverlayTitle
        {
            public RankingsTitle()
            {
                Title = "排名";
                Description = "來看看現在的最強的玩家是誰";
                IconTexture = "Icons/Hexacons/rankings";
            }
        }
    }

    public enum RankingsScope
    {
        [Description("成績")]
        Performance,
        [Description("月賽")]
        Spotlights,
        [Description("總分")]
        Score,
        [Description("國家")]
        Country
    }
}
