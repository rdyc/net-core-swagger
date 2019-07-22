using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Application.Responses
{
    public class CollectionResponse<T> : ListResponse<T> where T : IBaseModel
    {
        public Metadata Metadata { get; private set; }
    }
}