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
    /// Artist controller
    /// </summary>
    [Produces("application/json"), Consumes("application/json")]
    [ApiVersion("1"), Route("v{version:apiVersion}/artists")]
    [SwaggerTag("Create, read, update and delete artists")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService artist;
        private readonly IMapper mapper;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="artist"></param>
        /// <param name="mapper"></param>
        public ArtistController(IArtistService artist, IMapper mapper)
        {
            this.artist = artist ?? throw new ArgumentNullException(nameof(artist));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all artists
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(OperationId = "ArtistGetAll")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(CollectionResponse<ArtistModel>))]
        public async Task<IActionResult> Get([FromQuery]QueryRequest<ArtistField> request)
        {
            var data = await artist.Get(request);

            return Ok(mapper.Map<CollectionResponse<ArtistModel>>(data));
        }

        /// <summary>
        /// Get specific artist
        /// </summary>
        /// <param name="artistId">The artist id</param>
        /// <returns></returns>
        [HttpGet("{artistId}")]
        [SwaggerOperation(OperationId = "ArtistGetById")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(SingleResponse<ArtistModel>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string artistId)
        {
            var data = await artist.Get(artistId);
            
            if (data == null)
                return NotFound();

            return Ok(mapper.Map<SingleResponse<ArtistModel>>(data));
        }

        /// <summary>
        /// Add new artist
        /// </summary>
        /// <param name="payload">The payload</param>
        /// <returns></returns>
        [Obsolete, HttpPost]
        [SwaggerOperation(
            Description = "Please use V2 instead",
            OperationId = "ArtistPost")]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(SingleResponse<ArtistModel>))]
        public async Task<IActionResult> Post([FromBody]ArtistPostModel payload)
        {
            var created = await artist.Add(payload);

            return Created(string.Empty, mapper.Map<SingleResponse<ArtistModel>>(created));
        }

        /// <summary>
        /// Modify existing artist
        /// </summary>
        /// <param name="artistId">The artist id</param>
        /// <param name="payload">The payload</param>
        /// <returns></returns>
        [HttpPut("{artistId}")]
        [SwaggerOperation(OperationId = "ArtistPut")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, Type = typeof(SingleResponse<ArtistModel>))]
        public async Task<IActionResult> Put(string artistId, [FromBody]ArtistPutModel payload)
        {
            var updated = await artist.Update(payload.WithId(artistId));

            return Accepted(mapper.Map<SingleResponse<ArtistModel>>(updated));
        }

        /// <summary>
        /// Remove existing artist
        /// </summary>
        /// <param name="artistId">The artist id</param>
        /// <returns></returns>
        [HttpDelete("{artistId}")]
        [SwaggerOperation(OperationId = "ArtistDelete")]
        [SwaggerResponse((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> Delete(string artistId)
        {
            await artist.Delete(artistId);

            return Accepted();
        }
    }
}