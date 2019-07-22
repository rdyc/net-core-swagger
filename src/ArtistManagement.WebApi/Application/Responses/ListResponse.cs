using System.Collections.Generic;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Application.Responses
{
    public class ListResponse<T> where T : IBaseModel
    {
        public IEnumerable<T> Data { get; private set; }
    }
}