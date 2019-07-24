namespace ArtistManagement.WebApi.V1.Models
{
    public class ArtistPutModel : ArtistPostModel
    {
        public string Id { get; private set; }

        public ArtistPutModel WithId (string id) 
        {
            Id = id;

            return this;
        }
    }
}