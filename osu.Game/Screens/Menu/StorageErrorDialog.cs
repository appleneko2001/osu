// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Game.IO;
using osu.Game.Overlays;
using osu.Game.Overlays.Dialog;

namespace osu.Game.Screens.Menu
{
    public class StorageErrorDialog : PopupDialog
    {
        [Resolved]
        private DialogOverlay dialogOverlay { get; set; }

        [Resolved]
        private OsuGameBase osuGame { get; set; }

        public StorageErrorDialog(OsuStorage storage, OsuStorageError error)
        {
            HeaderText = "osu! �x�s���~";
            Icon = FontAwesome.Solid.ExclamationTriangle;

            var buttons = new List<PopupDialogButton>();

            switch (error)
            {
                case OsuStorageError.NotAccessible:
                    BodyText = $"���w����m (\"{storage.CustomStoragePath}\") �L�k�Q�X��. �p�G�ɦV���ؿ��O�~�m�w��, �гs����A�դ@��.";

                    buttons.AddRange(new PopupDialogButton[]
                    {
                        new PopupDialogCancelButton
                        {
                            Text = "�A�դ@��",
                            Action = () =>
                            {
                                if (!storage.TryChangeToCustomStorage(out var nextError))
                                    dialogOverlay.Push(new StorageErrorDialog(storage, nextError));
                            }
                        },
                        new PopupDialogCancelButton
                        {
                            Text = "�����q�{��m ����U�@���}�l�C��",
                        },
                        new PopupDialogOkButton
                        {
                            Text = "�_���q�{��m",
                            Action = storage.ResetCustomStoragePath
                        },
                    });
                    break;

                case OsuStorageError.AccessibleButEmpty:
                    BodyText = $"���w����m (\"{storage.CustomStoragePath}\") �w�g�@�L�Ҧ�, �p�G��L���ಾ���L��m�F, ���� osu! ��L���ಾ�^�ӫ�A��.";

                    // Todo: Provide the option to search for the files similar to migration.
                    buttons.AddRange(new PopupDialogButton[]
                    {
                        new PopupDialogCancelButton
                        {
                            Text = "�b��e��m���s�}�l"
                        },
                        new PopupDialogOkButton
                        {
                            Text = "�_���q�{��m",
                            Action = storage.ResetCustomStoragePath
                        },
                    });

                    break;
            }

            Buttons = buttons;
        }
    }
}
