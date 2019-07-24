using System;
using System.Net;
using System.Threading.Tasks;
using ArtistManagement.WebApi.Application.Fields;
using ArtistManagement.WebApi.Application.Requests;
using ArtistManagement.WebApi.Application.Responses;
using ArtistManagement.WebApi.Application.Services;
using ArtistManagement.WebApi.V1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ArtistManagement.WebApi.V1.Controllers
{
    /// <summary>
    /// Track controller
    /// </summary>
    [Produces("application/json"), Consumes("application/json")]
    [ApiVersion("1"), Route("v{version:apiVersion}/tracks")]
    [SwaggerTag("Create, read, update and delete tracks")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService track;
        private readonly IMapper mapper;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="track"></param>
        /// <param name="mapper"></param>
        public TrackController(ITrackService track, IMapper mapper)
        {
            this.track = track ?? throw new ArgumentNullException(nameof(track));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all tracks
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "TrackGetAll")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(CollectionResponse<TrackModel>))]
        public async Task<IActionResult> Get([FromQuery]QueryRequest<TrackField> request)
        {
            var data = await track.Get(request);

            return Ok(mapper.Map<CollectionResponse<TrackModel>>(data));
        }

        /// <summary>
        /// Get specific track
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        [HttpGet("{trackId}")]
        [SwaggerOperation(OperationId = "TrackGetById")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(SingleResponse<TrackModel>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string trackId)
        {
            var data = await track.Get(trackId);
            
            if (data == null)
                return NotFound();

            return Ok(mapper.Map<SingleResponse<TrackModel>>(data));
        }

        /// <summary>
        /// Create a new track
        /// </summary>
        /// <param name="payload">The payload</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "TrackPost")]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(SingleResponse<TrackModel>))]
        public async Task<IActionResult> Post([FromBody]TrackPostModel payload)
        {
            var created = await track.Add(payload);

            return Created(string.Empty, mapper.Map<SingleResponse<TrackModel>>(created));
        }

        /// <summary>
        /// Modify existing track
        /// </summary>
        /// <param name="trackId">The track id</param>
        /// <param name="payload">The payload</param>
        /// <returns></returns>
        [HttpPut("{trackId}")]
        [SwaggerOperation(OperationId = "TrackPut")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, Type = typeof(SingleResponse<TrackModel>))]
        public async Task<IActionResult> Put(string trackId, [FromBody]TrackPutModel payload)
        {
            var updated = await track.Update(payload.WithId(trackId));

            return Accepted(mapper.Map<SingleResponse<TrackModel>>(updated));
        }

        /// <summary>
        /// Remove existing track
        /// </summary>
        /// <param name="trackId">The track id</param>
        /// <returns></returns>
        [Obsolete, HttpDelete("{trackId}")]
        [SwaggerOperation(OperationId = "TrackDelete")]
        [SwaggerResponse((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> Delete(string trackId)
        {
            await track.Delete(trackId);

            return Accepted();
        }
    }
}