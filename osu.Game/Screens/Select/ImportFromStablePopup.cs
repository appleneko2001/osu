// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Sprites;
using osu.Game.Overlays.Dialog;

namespace osu.Game.Screens.Select
{
    public class ImportFromStablePopup : PopupDialog
    {
        public ImportFromStablePopup(Action importFromStable)
        {
            HeaderText = @"未安裝任何圖譜!";
            BodyText = "已發現正常版的 osu! 安裝位置\n要匯入圖譜, 成績, 圖譜收藏和皮膚嗎?\n這將在硬碟上爲這些檔案建立副本.";

            Icon = FontAwesome.Solid.Plane;

            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = @"是的, 麻煩了!",
                    Action = importFromStable
                },
                new PopupDialogCancelButton
                {
                    Text = @"不用了 我想要一個新的開始.",
                },
            };
        }
    }
}
