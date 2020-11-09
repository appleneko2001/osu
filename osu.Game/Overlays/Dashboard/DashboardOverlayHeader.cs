// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Overlays.Dashboard
{
    public class DashboardOverlayHeader : TabControlOverlayHeader<DashboardOverlayTabs>
    {
        protected override OverlayTitle CreateTitle() => new DashboardTitle();

        private class DashboardTitle : OverlayTitle
        {
            public DashboardTitle()
            {
                Title = "儀表板";
                Description = "查看好友和其他的資訊";
                IconTexture = "Icons/Hexacons/social";
            }
        }
    }

    public enum DashboardOverlayTabs
    {
        [Description("好友")]
        Friends,

        [Description("遊戲中")]
        CurrentlyPlaying
    }
}
