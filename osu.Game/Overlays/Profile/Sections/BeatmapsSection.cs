// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Online.API.Requests;
using osu.Game.Overlays.Profile.Sections.Beatmaps;

namespace osu.Game.Overlays.Profile.Sections
{
    public class BeatmapsSection : ProfileSection
    {
        public override string Title => "圖譜";

        public override string Identifier => "beatmaps";

        public BeatmapsSection()
        {
            Children = new[]
            {
                new PaginatedBeatmapContainer(BeatmapSetType.Favourite, User, "收藏的圖譜"),
                new PaginatedBeatmapContainer(BeatmapSetType.RankedAndApproved, User, "已進榜 & 合格的圖譜"),
                new PaginatedBeatmapContainer(BeatmapSetType.Loved, User, "最愛的圖譜"),
                new PaginatedBeatmapContainer(BeatmapSetType.Unranked, User, "提交中的圖譜"),
                new PaginatedBeatmapContainer(BeatmapSetType.Graveyard, User, "已拋棄的圖譜"),
            };
        }
    }
}
