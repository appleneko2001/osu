// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Sprites;
using osu.Game.Overlays.Dialog;

namespace osu.Game.Screens.Menu
{
    public class ConfirmExitDialog : PopupDialog
    {
        public ConfirmExitDialog(Action confirm, Action cancel)
        {
            HeaderText = "要準備離開了嗎 ?";
            BodyText = "還有機會改變主意!";

            Icon = FontAwesome.Solid.ExclamationTriangle;

            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = @"再見",
                    Action = confirm
                },
                new PopupDialogCancelButton
                {
                    Text = @"想再停留一會",
                    Action = cancel
                },
            };
        }
    }
}
