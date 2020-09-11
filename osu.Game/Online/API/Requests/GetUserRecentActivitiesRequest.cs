// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.ComponentModel;
using osu.Game.Online.API.Requests.Responses;

namespace osu.Game.Online.API.Requests
{
    public class GetUserRecentActivitiesRequest : PaginatedAPIRequest<List<APIRecentActivity>>
    {
        private readonly long userId;

        public GetUserRecentActivitiesRequest(long userId, int page = 0, int itemsPerPage = 5)
            : base(page, itemsPerPage)
        {
            this.userId = userId;
        }

        protected override string Target => $"users/{userId}/recent_activity";
    }

    public enum RecentActivityType
    {
        Achievement,
        BeatmapPlaycount,
        BeatmapsetApprove,
        BeatmapsetDelete,
        BeatmapsetRevive,
        BeatmapsetUpdate,
        BeatmapsetUpload,
        Medal,
        Rank,
        RankLost,
        UserSupportAgain,
        UserSupportFirst,
        UserSupportGift,
        UsernameChange,
    }

    public enum BeatmapApproval
    {
        [Description("進榜")]
        Ranked,
        [Description("批准")]
        Approved,
        [Description("合格")]
        Qualified,
        [Description("最愛")]
        Loved
    }
}
