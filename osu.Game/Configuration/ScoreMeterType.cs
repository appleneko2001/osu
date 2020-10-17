// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Configuration
{
    public enum ScoreMeterType
    {
        [Description("關閉")]
        None,

        [Description("打擊失誤計 (左)")]
        HitErrorLeft,

        [Description("打擊失誤計 (右)")]
        HitErrorRight,

        [Description("打擊失誤計 (下)")]
        HitErrorBottom,

        [Description("打擊失誤計 (左側和右側)")]
        HitErrorBoth,

        [Description("彩塊 (左)")]
        ColourLeft,

        [Description("彩塊 (右)")]
        ColourRight,

        [Description("彩塊 (左側和右側)")]
        ColourBoth,

        [Description("彩塊 (下)")]
        ColourBottom,
    }
}
