﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Overlays.Dashboard.Friends
{
    public enum OnlineStatus
    {
        [Description("所有")]
        All,
        [Description("在線")]
        Online,
        [Description("離線")]
        Offline
    }
}
