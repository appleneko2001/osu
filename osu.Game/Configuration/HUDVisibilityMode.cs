﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Configuration
{
    public enum HUDVisibilityMode
    {
        Never,

        [Description("遊戲中隱藏")]
        HideDuringGameplay,

        [Description("休息中隱藏")]
        HideDuringBreaks,

        Always
    }
}
