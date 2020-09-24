// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using Humanizer;
using osu.Framework.Graphics.Sprites;
using osu.Game.Overlays.Dialog;

namespace osu.Game.Collections
{
    public class DeleteCollectionDialog : PopupDialog
    {
        public DeleteCollectionDialog(BeatmapCollection collection, Action deleteAction)
        {
            HeaderText = "確認刪除收藏";
            BodyText = $"{collection.Name.Value} ({collection.Beatmaps.Count} 個圖譜)"; // {"beatmap".ToQuantity(collection.Beatmaps.Count)}

            Icon = FontAwesome.Regular.TrashAlt;

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
