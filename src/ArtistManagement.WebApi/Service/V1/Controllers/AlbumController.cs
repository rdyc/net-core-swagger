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
    [Route("v1/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService service;
        private readonly IMapper mapper;

        public AlbumController(IAlbumService service, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]QueryRequest<AlbumField> request)
        {
            var data = await service.Get(request);

            return Ok(mapper.Map<CollectionResponse<AlbumModel>>(data));
        }

        [HttpGet("{albumId}")]
        public async Task<IActionResult> Get(string albumId)
        {
            var data = await service.Get(albumId);
            
            if (data == null)
                return NotFound();

            return Ok(mapper.Map<SingleResponse<AlbumModel>>(data));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AlbumPostModel payload)
        {
            var created = await service.Add(payload);

            return Created(string.Empty, mapper.Map<SingleResponse<AlbumModel>>(created));
        }

        [HttpPut("{albumId}")]
        public async Task<IActionResult> Put(string albumId, [FromBody]AlbumPutModel payload)
        {
            var updated = await service.Update(payload.WithId(albumId));

            return Accepted(mapper.Map<SingleResponse<AlbumModel>>(updated));
        }

        [HttpDelete("{albumId}")]
        public async Task<IActionResult> Delete(string albumId)
        {
            await service.Delete(albumId);

            return Accepted();
        }
    }
}