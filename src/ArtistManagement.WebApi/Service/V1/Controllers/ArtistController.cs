using System;
using System.Threading.Tasks;
using ArtistManagement.WebApi.Application.Fields;
using ArtistManagement.WebApi.Application.Requests;
using ArtistManagement.WebApi.Application.Responses;
using ArtistManagement.WebApi.Application.Services;
using ArtistManagement.WebApi.V1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArtistManagement.WebApi.V1.Controllers
{
    [Route("v1/artists")]
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
        public async Task<IActionResult> Get([FromQuery]QueryRequest<ArtistField> request)
        {
            var data = await artist.Get(request);

            return Ok(mapper.Map<CollectionResponse<ArtistModel>>(data));
        }

        [HttpGet("{artistId}")]
        public async Task<IActionResult> Get(string artistId)
        {
            var data = await artist.Get(artistId);
            
            if (data == null)
                return NotFound();

            return Ok(mapper.Map<SingleResponse<ArtistModel>>(data));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ArtistPostModel payload)
        {
            var created = await artist.Add(payload);

            return Created(string.Empty, mapper.Map<SingleResponse<ArtistModel>>(created));
        }

        [HttpPut("{artistId}")]
        public async Task<IActionResult> Put(string artistId, [FromBody] ArtistPutModel payload)
        {
            var updated = await artist.Update(payload.WithId(artistId));

            return Accepted(mapper.Map<SingleResponse<ArtistModel>>(updated));
        }

        [HttpDelete("{artistId}")]
        public async Task<IActionResult> Delete(string artistId)
        {
            await artist.Delete(artistId);

            return Accepted();
        }
    }
}