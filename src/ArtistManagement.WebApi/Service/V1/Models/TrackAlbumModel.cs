using System;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    public class TrackAlbumModel : BaseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Release { get; set; }
    }
}