using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Application.Responses
{
    public class CollectionResponse<T> : ListResponse<T> where T : BaseModel
    {
        public Meta Meta { get; private set; }
    }
}