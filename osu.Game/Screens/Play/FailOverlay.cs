// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Graphics;
using osuTK.Graphics;
using osu.Framework.Allocation;

namespace osu.Game.Screens.Play
{
    public class FailOverlay : GameplayMenuOverlay
    {
        public override string Header => "失敗";
        public override string Description => "啊咧? 重新開始嗎";

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            AddButton("重新開始", colours.YellowDark, () => OnRetry?.Invoke());
            AddButton("離開", new Color4(170, 27, 39, 255), () => OnQuit?.Invoke());
        }
    }
}
