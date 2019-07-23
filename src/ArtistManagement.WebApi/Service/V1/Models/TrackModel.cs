using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    public class TrackModel : BaseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public ArtistModel Artist { get; set; }
    }
}