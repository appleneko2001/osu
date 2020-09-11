// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Sprites;
using osu.Game.Overlays.Dialog;

namespace osu.Game.Overlays.Settings.Sections.Maintenance
{
    public class DeleteAllBeatmapsDialog : PopupDialog
    {
        public DeleteAllBeatmapsDialog(Action deleteAction)
        {
            BodyText = "所有已安裝的圖譜嗎?";

            Icon = FontAwesome.Regular.TrashAlt;
            HeaderText = @"確認清除";
            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = @"是的, 去吧!",
                    Action = deleteAction
                },
                new PopupDialogCancelButton
                {
                    Text = @"不要! 取消任務!",
                },
            };
        }
    }
}
