using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Application.Responses
{
    public class SingleResponse<T> where T : BaseModel
    {
        public T Data { get; private set; }
    }
}