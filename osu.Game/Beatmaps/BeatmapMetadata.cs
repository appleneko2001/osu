﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using osu.Framework.Testing;
using osu.Game.Database;
using osu.Game.Users;

namespace osu.Game.Beatmaps
{
    [ExcludeFromDynamicCompile]
    [Serializable]
    public class BeatmapMetadata : IEquatable<BeatmapMetadata>, IHasPrimaryKey
    {
        public int ID { get; set; }

        public string Title { get; set; }

        [JsonProperty(@"title_unicode")] // Redirect property to title_unicode for access unicode title
        public string TitleUnicode { get; set; }
        public string Artist { get; set; }

        [JsonProperty(@"artist_unicode")] // Redirect property to artist_unicode for access unicode artist
        public string ArtistUnicode { get; set; }

        [JsonIgnore]
        public List<BeatmapInfo> Beatmaps { get; set; }

        [JsonIgnore]
        public List<BeatmapSetInfo> BeatmapSets { get; set; }

        /// <summary>
        /// Helper property to deserialize a username to <see cref="User"/>.
        /// </summary>
        [JsonProperty(@"creator")]
        [Column("Author")]
        public string AuthorString
        {
            get => Author?.Username;
            set => Author = new User { Username = value };
        }

        /// <summary>
        /// The author of the beatmaps in this set.
        /// </summary>
        [JsonIgnore]
        public User Author;

        public string Source { get; set; }

        [JsonProperty(@"tags")]
        public string Tags { get; set; }

        public int PreviewTime { get; set; }
        public string AudioFile { get; set; }
        public string BackgroundFile { get; set; }

        public override string ToString()
        {
            string author = Author == null ? string.Empty : $"({Author})";
            return $"{Artist} - {Title} {author}".Trim();
        }

        [JsonIgnore]
        public string[] SearchableTerms => new[]
        {
            Author?.Username,
            Artist,
            ArtistUnicode,
            Title,
            TitleUnicode,
            Source,
            Tags
        }.Where(s => !string.IsNullOrEmpty(s)).ToArray();

        public bool Equals(BeatmapMetadata other)
        {
            if (other == null)
                return false;

            return Title == other.Title
                   && TitleUnicode == other.TitleUnicode
                   && Artist == other.Artist
                   && ArtistUnicode == other.ArtistUnicode
                   && AuthorString == other.AuthorString
                   && Source == other.Source
                   && Tags == other.Tags
                   && PreviewTime == other.PreviewTime
                   && AudioFile == other.AudioFile
                   && BackgroundFile == other.BackgroundFile;
        }
    }
}
