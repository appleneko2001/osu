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
            HeaderText = "osu! 儲存錯誤";
            Icon = FontAwesome.Solid.ExclamationTriangle;

            var buttons = new List<PopupDialogButton>();

            switch (error)
            {
                case OsuStorageError.NotAccessible:
                    BodyText = $"指定的位置 (\"{storage.CustomStoragePath}\") 無法被訪問. 如果導向的目錄是外置硬碟, 請連接後再試一次.";

                    buttons.AddRange(new PopupDialogButton[]
                    {
                        new PopupDialogCancelButton
                        {
                            Text = "再試一次",
                            Action = () =>
                            {
                                if (!storage.TryChangeToCustomStorage(out var nextError))
                                    dialogOverlay.Push(new StorageErrorDialog(storage, nextError));
                            }
                        },
                        new PopupDialogCancelButton
                        {
                            Text = "先用默認位置 直到下一次開始遊戲",
                        },
                        new PopupDialogOkButton
                        {
                            Text = "復原默認位置",
                            Action = storage.ResetCustomStoragePath
                        },
                    });
                    break;

                case OsuStorageError.AccessibleButEmpty:
                    BodyText = $"指定的位置 (\"{storage.CustomStoragePath}\") 已經一無所有, 如果把他們轉移到其他位置了, 關閉 osu! 把他們轉移回來後再試.";

                    // Todo: Provide the option to search for the files similar to migration.
                    buttons.AddRange(new PopupDialogButton[]
                    {
                        new PopupDialogCancelButton
                        {
                            Text = "在當前位置重新開始"
                        },
                        new PopupDialogOkButton
                        {
                            Text = "復原默認位置",
                            Action = storage.ResetCustomStoragePath
                        },
                    });

                    break;
            }

            Buttons = buttons;
        }
    }
}
