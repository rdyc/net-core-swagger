using System;
using System.Collections.Generic;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    public class TrackModel : BaseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }
        public ArtistModel Artist { get; set; }
        public IEnumerable<TrackAlbumModel> Albums { get; set; }
    }
}