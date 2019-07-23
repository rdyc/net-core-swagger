namespace ArtistManagement.WebApi.V1.Models
{
    public class TrackPutModel : TrackPostModel
    {
        public string Id { get; private set;}

        public TrackPutModel WithId (string id) 
        {
            Id = id;

            return this;
        }
    }
}