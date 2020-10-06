// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Collections;
using osu.Game.Graphics.UserInterface;
using osu.Game.Scoring;
using osu.Game.Skinning;

namespace osu.Game.Overlays.Settings.Sections.Maintenance
{
    public class GeneralSettings : SettingsSubsection
    {
        protected override string Header => "一般設定";

        private TriangleButton importBeatmapsButton;
        private TriangleButton importScoresButton;
        private TriangleButton importSkinsButton;
        private TriangleButton importCollectionsButton;
        private TriangleButton deleteBeatmapsButton;
        private TriangleButton deleteScoresButton;
        private TriangleButton deleteSkinsButton;
        private TriangleButton restoreButton;
        private TriangleButton undeleteButton;

        [BackgroundDependencyLoader(permitNulls: true)]
        private void load(BeatmapManager beatmaps, ScoreManager scores, SkinManager skins, [CanBeNull] CollectionManager collectionManager, DialogOverlay dialogOverlay)
        {
            if (beatmaps.SupportsImportFromStable)
            {
                Add(importBeatmapsButton = new SettingsButton
                {
                    Text = "從正常版匯入所有圖譜",
                    Action = () =>
                    {
                        importBeatmapsButton.Enabled.Value = false;
                        beatmaps.ImportFromStableAsync().ContinueWith(t => Schedule(() => importBeatmapsButton.Enabled.Value = true));
                    }
                });
            }

            Add(deleteBeatmapsButton = new DangerousSettingsButton
            {
                Text = "清除所有已安裝的圖譜",
                Action = () =>
                {
                    dialogOverlay?.Push(new DeleteAllBeatmapsDialog(() =>
                    {
                        deleteBeatmapsButton.Enabled.Value = false;
                        Task.Run(() => beatmaps.Delete(beatmaps.GetAllUsableBeatmapSets())).ContinueWith(t => Schedule(() => deleteBeatmapsButton.Enabled.Value = true));
                    }));
                }
            });

            if (scores.SupportsImportFromStable)
            {
                Add(importScoresButton = new SettingsButton
                {
                    Text = "從正常版匯入成績",
                    Action = () =>
                    {
                        importScoresButton.Enabled.Value = false;
                        scores.ImportFromStableAsync().ContinueWith(t => Schedule(() => importScoresButton.Enabled.Value = true));
                    }
                });
            }

            Add(deleteScoresButton = new DangerousSettingsButton
            {
                Text = "清除所有本地成績",
                Action = () =>
                {
                    dialogOverlay?.Push(new DeleteAllBeatmapsDialog(() =>
                    {
                        deleteScoresButton.Enabled.Value = false;
                        Task.Run(() => scores.Delete(scores.GetAllUsableScores())).ContinueWith(t => Schedule(() => deleteScoresButton.Enabled.Value = true));
                    })
                    {
                        BodyText = "所有本地成績嗎?"
                    });
                }
            });

            if (skins.SupportsImportFromStable)
            {
                Add(importSkinsButton = new SettingsButton
                {
                    Text = "從正常版匯入皮膚",
                    Action = () =>
                    {
                        importSkinsButton.Enabled.Value = false;
                        skins.ImportFromStableAsync().ContinueWith(t => Schedule(() => importSkinsButton.Enabled.Value = true));
                    }
                });
            }

            Add(deleteSkinsButton = new DangerousSettingsButton
            {
                Text = "清除所有已安裝的皮膚",
                Action = () =>
                {
                    dialogOverlay?.Push(new DeleteAllBeatmapsDialog(() =>
                    {
                        deleteSkinsButton.Enabled.Value = false;
                        Task.Run(() => skins.Delete(skins.GetAllUserSkins())).ContinueWith(t => Schedule(() => deleteSkinsButton.Enabled.Value = true));
                    })
                    {
                        BodyText = "所有已安裝的皮膚嗎?"
                    });
                }
            });

            if (collectionManager != null)
            {
                if (collectionManager.SupportsImportFromStable)
                {
                    Add(importCollectionsButton = new SettingsButton
                    {
                        Text = "從正常版匯入圖譜收藏",
                        Action = () =>
                        {
                            importCollectionsButton.Enabled.Value = false;
                            collectionManager.ImportFromStableAsync().ContinueWith(t => Schedule(() => importCollectionsButton.Enabled.Value = true));
                        }
                    });
                }

                Add(new DangerousSettingsButton
                {
                    Text = "清除所有的圖譜收藏",
                    Action = () =>
                    {
                        dialogOverlay?.Push(new DeleteAllBeatmapsDialog(collectionManager.DeleteAll) { BodyText = "所有的圖譜收藏嗎?" });
                    }
                });
            }

            AddRange(new Drawable[]
            {
                restoreButton = new SettingsButton
                {
                    Text = "復原已隱藏的難度",
                    Action = () =>
                    {
                        restoreButton.Enabled.Value = false;
                        Task.Run(() =>
                        {
                            foreach (var b in beatmaps.QueryBeatmaps(b => b.Hidden).ToList())
                                beatmaps.Restore(b);
                        }).ContinueWith(t => Schedule(() => restoreButton.Enabled.Value = true));
                    }
                },
                undeleteButton = new SettingsButton
                {
                    Text = "復原最近刪除的圖譜",
                    Action = () =>
                    {
                        undeleteButton.Enabled.Value = false;
                        Task.Run(() => beatmaps.Undelete(beatmaps.QueryBeatmapSets(b => b.DeletePending).ToList())).ContinueWith(t => Schedule(() => undeleteButton.Enabled.Value = true));
                    }
                },
            });
        }
    }
}
