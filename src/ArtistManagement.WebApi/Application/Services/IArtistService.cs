using System.Threading.Tasks;
using ArtistManagement.WebApi.Application.Fields;
using ArtistManagement.WebApi.Application.Requests;
using ArtistManagement.WebApi.Application.Responses;
using ArtistManagement.WebApi.V1.Models;

namespace ArtistManagement.WebApi.Application.Services
{
    public interface IArtistService
    {
        Task<CollectionResponse<ArtistModel>> Get(QueryStringRequest<ArtistField> request);

        Task<SingleResponse<ArtistModel>> Get(string id);

        Task<SingleResponse<ArtistModel>> Add(ArtistPayloadModel payload);

        Task<SingleResponse<ArtistModel>> Update(ArtistPayloadModel payload);

        Task Delete(string id);
    }
}