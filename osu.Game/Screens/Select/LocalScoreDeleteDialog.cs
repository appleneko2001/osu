// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Game.Overlays.Dialog;
using osu.Game.Scoring;
using System.Diagnostics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Beatmaps;

namespace osu.Game.Screens.Select
{
    public class LocalScoreDeleteDialog : PopupDialog
    {
        private readonly ScoreInfo score;

        [Resolved]
        private ScoreManager scoreManager { get; set; }

        [Resolved]
        private BeatmapManager beatmapManager { get; set; }

        public LocalScoreDeleteDialog(ScoreInfo score)
        {
            this.score = score;
            Debug.Assert(score != null);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            BeatmapInfo beatmap = beatmapManager.QueryBeatmap(b => b.ID == score.BeatmapInfoID);
            Debug.Assert(beatmap != null);

            BodyText = $"{score.User} ({score.DisplayAccuracy}, {score.Rank})";

            Icon = FontAwesome.Regular.TrashAlt;
            HeaderText = "確認清理本地成績";
            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = "是的, 麻煩了.",
                    Action = () => scoreManager?.Delete(score)
                },
                new PopupDialogCancelButton
                {
                    Text = "不要, 我依依不捨.",
                },
            };
        }
    }
}
