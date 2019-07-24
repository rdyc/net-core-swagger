using System;
using System.Collections.Generic;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    /// <summary>
    /// Track
    /// </summary>
    public class TrackModel : BaseModel
    {
        /// <summary>
        /// The track Id
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// Title of track
        /// </summary>
        /// <value></value>
        public string Title { get; set; }

        /// <summary>
        /// Track genre
        /// </summary>
        /// <value></value>
        public string Genre { get; set; }

        /// <summary>
        /// Duration of track
        /// </summary>
        /// <value></value>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// The artist
        /// </summary>
        /// <value></value>
        public ArtistModel Artist { get; set; }

        /// <summary>
        /// Albums of the tracks
        /// </summary>
        /// <value></value>
        public IEnumerable<TrackAlbumModel> Albums { get; set; }
    }
}