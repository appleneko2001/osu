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
            HeaderText = "�n�x�s����Ъ��ܰʶ�?";

            Icon = FontAwesome.Regular.Save;

            Buttons = new PopupDialogButton[]
            {
                new PopupDialogCancelButton
                {
                    Text = @"�x�s�ڪ��ǧ@!",
                    Action = saveAndExit
                },
                new PopupDialogOkButton
                {
                    Text = @"�ѱ��Ҧ��ܰ�",
                    Action = exit
                },
                new PopupDialogCancelButton
                {
                    Text = @"��p, �ڷQ�~�򰵹�",
                },
            };
        }
    }
}
