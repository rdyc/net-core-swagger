using System.Threading.Tasks;
using ArtistManagement.WebApi.Application.Responses;
using ArtistManagement.WebApi.V1.Models;

namespace ArtistManagement.WebApi.Application.Services
{
    public interface IArtistService
    {
        Task<CollectionResponse<ArtistModel>> Get(int page, int size);

        Task<ListResponse<ArtistModel>> Get(string name, string category, int page, int size);

        Task<SingleResponse<ArtistModel>> Get(string id);

        Task<SingleResponse<ArtistModel>> Add(ArtistPayloadModel payload);

        Task<SingleResponse<ArtistModel>> Update(ArtistPayloadModel payload);

        Task<bool> Delete(string id);
    }
}