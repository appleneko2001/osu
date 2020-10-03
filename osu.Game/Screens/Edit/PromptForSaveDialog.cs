// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Sprites;
using osu.Game.Overlays.Dialog;

namespace osu.Game.Screens.Edit
{
    public class PromptForSaveDialog : PopupDialog
    {
        public PromptForSaveDialog(Action exit, Action saveAndExit)
        {
            HeaderText = "要儲存對圖譜的變動嗎?";

            Icon = FontAwesome.Regular.Save;

            Buttons = new PopupDialogButton[]
            {
                new PopupDialogCancelButton
                {
                    Text = @"儲存我的傑作!",
                    Action = saveAndExit
                },
                new PopupDialogOkButton
                {
                    Text = @"忘掉所有變動",
                    Action = exit
                },
                new PopupDialogCancelButton
                {
                    Text = @"抱歉, 我想繼續做圖",
                },
            };
        }
    }
}
