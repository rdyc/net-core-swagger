using System;
using System.Collections.Generic;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    public class AlbumPostModel : BaseModel
    {
        public string Name { get; set; }
        public DateTime Release { get; set; }
        public IEnumerable<string> TrackIds { get; set; }
    }
}