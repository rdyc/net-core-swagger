using System.Threading.Tasks;
using ArtistManagement.WebApi.Application.Fields;
using ArtistManagement.WebApi.Application.Responses;
using ArtistManagement.WebApi.Infrastructure;
using ArtistManagement.WebApi.V1.Models;

namespace ArtistManagement.WebApi.Application.Services
{
    public interface IArtistService : IService<ArtistModel, ArtistPostModel, ArtistPutModel, ArtistField>
    {
        Task<SingleResponse<ArtistModel>> Get(string id);
    }
}