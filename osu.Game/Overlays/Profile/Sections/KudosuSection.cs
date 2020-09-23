// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Game.Overlays.Profile.Sections.Kudosu;

namespace osu.Game.Overlays.Profile.Sections
{
    public class KudosuSection : ProfileSection
    {
        public override string Title => "Kudosu!";

        public override string Identifier => "kudosu";

        public KudosuSection()
        {
            Children = new Drawable[]
            {
                new KudosuInfo(User),
                //  這段代碼因原Repo變動 暫時被覆蓋
                //  new PaginatedKudosuHistoryContainer(User, null, @"該用戶近期沒有收到任何的 kudosu!"),
                new PaginatedKudosuHistoryContainer(User),
            };
        }
    }
}
