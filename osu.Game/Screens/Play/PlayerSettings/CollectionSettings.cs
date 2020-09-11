// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Overlays.Music;

namespace osu.Game.Screens.Play.PlayerSettings
{
    public class CollectionSettings : PlayerSettingsGroup
    {
        public CollectionSettings()
            : base("收藏設定")
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new OsuSpriteText
                {
                    Text = @"將當前曲目加入到",
                },
                new CollectionsDropdown<PlaylistCollection>
                {
                    RelativeSizeAxes = Axes.X,
                    Items = new[] { PlaylistCollection.All },
                },
            };
        }
    }
}
