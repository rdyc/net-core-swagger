using System;
using System.Threading.Tasks;
using ArtistManagement.WebApi.Application.Responses;
using ArtistManagement.WebApi.Application.Services;
using ArtistManagement.WebApi.V1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArtistManagement.WebApi.V1.Controllers
{
    [Route("api/v1/artists")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService artist;
        private readonly IMapper mapper;

        public ArtistController(IArtistService artist, IMapper mapper)
        {
            this.artist = artist ?? throw new ArgumentNullException(nameof(artist));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<CollectionResponse<ArtistModel>>> Get(int page = 1, int size = 10)
        {
            var data = await artist.Get(page, size);

            return mapper.Map<CollectionResponse<ArtistModel>>(data);
        }

        [HttpGet("list")]
        public async Task<ActionResult<ListResponse<ArtistModel>>> Get(string name, string category, int page, int size)
        {
            var data = await artist.Get(name, category, page, size);

            return mapper.Map<CollectionResponse<ArtistModel>>(data);
        }

        [HttpGet("{artistId}")]
        public async Task<ActionResult<SingleResponse<ArtistModel>>> Get(string artistId)
        {
            var data = await artist.Get(artistId);

            return mapper.Map<SingleResponse<ArtistModel>>(data);
        }

        [HttpPost]
        public async Task<ActionResult<SingleResponse<ArtistModel>>> Post([FromBody] ArtistPayloadModel payload)
        {
            var created = await artist.Add(payload);

            return mapper.Map<SingleResponse<ArtistModel>>(created);
        }

        [HttpPut]
        public async Task<ActionResult<SingleResponse<ArtistModel>>> Put([FromBody] ArtistPayloadModel payload)
        {
            var updated = await artist.Add(payload);

            return mapper.Map<SingleResponse<ArtistModel>>(updated);
        }

        [HttpDelete]
        public async Task<bool> Delete(string id)
        {
            var deleted = await artist.Delete(id);

            return deleted;
        }
    }
}