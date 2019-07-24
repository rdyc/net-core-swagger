using System;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    /// <summary>
    /// Track Album
    /// </summary>
    public class TrackAlbumModel : BaseModel
    {
        /// <summary>
        /// The album id
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// Name of album
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Release date
        /// </summary>
        /// <value></value>
        public DateTime Release { get; set; }
    }
}