using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    public class TrackPostModel : BaseModel
    {
        public string ArtistId { get; set; }
        public string Title { get; set; }
    }
}