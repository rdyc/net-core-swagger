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
    [Route("v1/tracks")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService track;
        private readonly IMapper mapper;

        public TrackController(ITrackService track, IMapper mapper)
        {
            this.track = track ?? throw new ArgumentNullException(nameof(track));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]QueryRequest<TrackField> request)
        {
            var data = await track.Get(request);

            return Ok(mapper.Map<CollectionResponse<TrackModel>>(data));
        }

        [HttpGet("{trackId}")]
        public async Task<IActionResult> Get(string trackId)
        {
            var data = await track.Get(trackId);
            
            if (data == null)
                return NotFound();

            return Ok(mapper.Map<SingleResponse<TrackModel>>(data));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TrackPostModel payload)
        {
            var created = await track.Add(payload);

            return Created(string.Empty, mapper.Map<SingleResponse<TrackModel>>(created));
        }

        [HttpPut("{trackId}")]
        public async Task<IActionResult> Put(string trackId, [FromBody] TrackPutModel payload)
        {
            var updated = await track.Update(payload.WithId(trackId));

            return Accepted(mapper.Map<SingleResponse<TrackModel>>(updated));
        }

        [HttpDelete("{trackId}")]
        public async Task<IActionResult> Delete(string trackId)
        {
            await track.Delete(trackId);

            return Accepted();
        }
    }
}