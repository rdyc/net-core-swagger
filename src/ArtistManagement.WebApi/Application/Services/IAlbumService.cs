using System.Threading.Tasks;
using ArtistManagement.WebApi.Application.Fields;
using ArtistManagement.WebApi.Application.Responses;
using ArtistManagement.WebApi.Infrastructure;
using ArtistManagement.WebApi.V1.Models;

namespace ArtistManagement.WebApi.Application.Services
{
    public interface IAlbumService : IService<AlbumModel, AlbumPostModel, AlbumPutModel, AlbumField>
    {
        Task<SingleResponse<AlbumModel>> Get(string id);
    }
}