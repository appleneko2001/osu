// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Sprites;
using osu.Game.Overlays.Dialog;

namespace osu.Game.Overlays.Chat
{
    public class ExternalLinkDialog : PopupDialog
    {
        public ExternalLinkDialog(string url, Action openExternalLinkAction)
        {
            HeaderText = "只是確認一下...";
            BodyText = $"你/妳現在在離開 osu! 並訪問下面的網址:\n\n{url}";

            Icon = FontAwesome.Solid.ExclamationTriangle;

            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = @"我確認要訪問這個網址.",
                    Action = openExternalLinkAction
                },
                new PopupDialogCancelButton
                {
                    Text = @"不是! 取消任務!"
                },
            };
        }
    }
}
