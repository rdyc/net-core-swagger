using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    public class ArtistModel : IBaseModel
    {
        public string Id { get; set;}
        public string Name { get; set;}
        public string Nationality { get; set;}
        public string Genre { get; set;}
    }
}