// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Screens.Edit
{
    public enum EditorScreenMode
    {
        [Description("設定")]
        SongSetup,

        [Description("作曲")]
        Compose,

        [Description("設計")]
        Design,

        [Description("校時")]  // [RequestImprove] 原文是Timing 跟GlobalActionContainer.cs的情況一樣
        Timing,
    }
}
