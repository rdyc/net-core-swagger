using System;
using System.Collections.Generic;
using ArtistManagement.WebApi.Infrastructure;
using Newtonsoft.Json;

namespace ArtistManagement.WebApi.V1.Models
{
    /// <summary>
    /// Album
    /// </summary>
    public class AlbumModel : BaseModel
    {
        /// <summary>
        /// The album id
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// The name of album
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Date of release
        /// </summary>
        /// <value></value>
        [JsonConverter(typeof(DateFormatConverter))]
        public DateTime Release { get; set; }

        /// <summary>
        /// Tracks in album
        /// </summary>
        /// <value></value>
        public IEnumerable<AlbumTrackModel> Tracks { get; set; }
    }
}