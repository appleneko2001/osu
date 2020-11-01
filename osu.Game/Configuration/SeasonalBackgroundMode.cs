// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;

namespace osu.Game.Configuration
{
    public enum SeasonalBackgroundMode
    {
        /// <summary>
        /// Seasonal backgrounds are shown regardless of season, if at all available.
        /// </summary>
        Always,

        /// <summary>
        /// Seasonal backgrounds are shown only during their corresponding season.
        /// </summary>
        [Description("有時候")]
        Sometimes,

        /// <summary>
        /// Seasonal backgrounds are never shown.
        /// </summary>
        Never
    }
}
