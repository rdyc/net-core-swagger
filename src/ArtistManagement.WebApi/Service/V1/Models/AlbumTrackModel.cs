using System;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    /// <summary>
    /// Album track
    /// </summary>
    public class AlbumTrackModel : BaseModel
    {
        /// <summary>
        /// The track id
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
    }
}