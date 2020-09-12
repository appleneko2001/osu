// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Overlays.BeatmapListing
{
    public enum SortCriteria
    {
        [Description("���D")]
        Title,
        [Description("�t�X��")]
        Artist,
        [Description("����")]
        Difficulty,
        [Description("�w�i�]")]
        Ranked,
        [Description("����")]
        Rating,
        [Description("�C������")]
        Plays,
        [Description("���w")]
        Favourites,
        Relevance // [RequestImprove]
    }
}
