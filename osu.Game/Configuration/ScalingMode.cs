// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Configuration
{
    public enum ScalingMode
    {
        [Description("關閉")]
        Off,

        [Description("所有")]
        Everything,

        [Description("除圖層")]
        ExcludeOverlays,

        [Description("遊戲中")]
        Gameplay
    }
}
