using System;
using System.Linq;
using System.Threading.Tasks;
using ArtistManagement.WebApi.Application.Responses;
using ArtistManagement.WebApi.Domain.Entities;
using ArtistManagement.WebApi.Domain.Repositories;
using ArtistManagement.WebApi.Infrastructure;
using ArtistManagement.WebApi.V1.Models;
using AutoMapper;

namespace ArtistManagement.WebApi.Application.Services
{
    internal class ArtistService : IArtistService
    {
        private readonly IArtistRepository repository;
        private readonly IMapper mapper;

        public ArtistService(IArtistRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CollectionResponse<ArtistModel>> Get(int page, int size)
        {
            if (page < 1) page = 1;

            var data = repository.Get();

            var dto = new CollectionDto<ArtistEntity>
            {
                Total = data.Count(),
                Size = size,
                Data = data.Skip((page - 1) * size).Take(size).AsEnumerable()
            };

            dto.SetPage(page);

            var result = mapper.Map<CollectionResponse<ArtistModel>>(dto);

            return await Task.FromResult(result);
        }

        public async Task<ListResponse<ArtistModel>> Get(string name, string category, int page, int size)
        {
            var data = repository.Get();

            if(string.IsNullOrEmpty(name))
                data = data.Where(e => e.Name.Equals(name));

            var result = mapper.Map<CollectionResponse<ArtistModel>>(data);

            return await Task.FromResult(result);
        }

        public Task<SingleResponse<ArtistModel>> Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<SingleResponse<ArtistModel>> Add(ArtistPayloadModel payload)
        {
            throw new System.NotImplementedException();
        }

        public Task<SingleResponse<ArtistModel>> Update(ArtistPayloadModel payload)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}