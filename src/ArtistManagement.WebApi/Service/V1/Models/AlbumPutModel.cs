namespace ArtistManagement.WebApi.V1.Models
{
    public class AlbumPutModel : AlbumPostModel
    {
        public string Id { get; private set; }

        public AlbumPutModel WithId (string id) 
        {
            Id = id;

            return this;
        }
    }
}