using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    public class ArtistPayloadModel : BaseModel
    {
        public string Id { get; private set;}
        public string Name { get; set;}
        public string Nationality { get; set;}
        public string Genre { get; set;}

        public ArtistPayloadModel WithId (string id) 
        {
            Id = id;

            return this;
        }
    }
}