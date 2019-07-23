using System;
using System.Threading.Tasks;
using ArtistManagement.WebApi.Application.Requests;
using ArtistManagement.WebApi.Application.Responses;

namespace ArtistManagement.WebApi.Infrastructure
{
    public interface IService<M, C, U, F> 
        where M : BaseModel 
        where C : BaseModel 
        where U : BaseModel 
        where F : struct, IConvertible
    {
        Task<CollectionResponse<M>> Get(QueryRequest<F> request);

        Task<SingleResponse<M>> Add(C payload);

        Task<SingleResponse<M>> Update(U payload);

        Task Delete(string id);
    }
}