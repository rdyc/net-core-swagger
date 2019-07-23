using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    public class ArtistPostModel : BaseModel
    {
        public string Name { get; set;}
        public string Nationality { get; set;}
    }
}