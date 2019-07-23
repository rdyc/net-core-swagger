using System;
using System.Collections.Generic;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    public class AlbumModel : BaseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Release { get; set; }
        public IEnumerable<AlbumTrackModel> Tracks { get; set; }
    }
}