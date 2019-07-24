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
    /// Album controller
    /// </summary>
    [Produces("application/json"), Consumes("application/json")]
    [ApiVersion("1"), Route("v{version:apiVersion}/albums")]
    [SwaggerTag("Create, read, update and delete albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService service;
        private readonly IMapper mapper;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="service"></param>
        /// <param name="mapper"></param>
        public AlbumController(IAlbumService service, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all albums
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(
            Description = "All albums description goes here", 
            OperationId = "AlbumGetAll"
        )]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(CollectionResponse<AlbumModel>))]
        public async Task<IActionResult> Get([FromQuery]QueryRequest<AlbumField> request)
        {
            var data = await service.Get(request);

            return Ok(mapper.Map<CollectionResponse<AlbumModel>>(data));
        }

        /// <summary>
        /// Get specific album
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        [HttpGet("{albumId}")]
        [SwaggerOperation(
            Description = "Album description goes here", 
            OperationId = "AlbumGetById")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(SingleResponse<AlbumModel>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string albumId)
        {
            var data = await service.Get(albumId);
            
            if (data == null)
                return NotFound();

            return Ok(mapper.Map<SingleResponse<AlbumModel>>(data));
        }

        /// <summary>
        /// Create a new album
        /// </summary>
        /// <param name="payload">The payload</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(
            Description = "Create album description goes here", 
            OperationId = "AlbumPost")]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(SingleResponse<AlbumModel>))]
        public async Task<IActionResult> Post([FromBody]AlbumPostModel payload)
        {
            var created = await service.Add(payload);

            return Created(string.Empty, mapper.Map<SingleResponse<AlbumModel>>(created));
        }

        /// <summary>
        /// Modify existing album
        /// </summary>
        /// <param name="albumId">The album id</param>
        /// <param name="payload">The payload</param>
        /// <returns></returns>
        [HttpPut("{albumId}")]
        [SwaggerOperation(
            Description = "Modify album description goes here", 
            OperationId = "AlbumPut")]
        [SwaggerResponse((int)HttpStatusCode.Accepted, Type = typeof(SingleResponse<AlbumModel>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(string albumId, [FromBody]AlbumPutModel payload)
        {
            var isValid = await service.IsValidAsync(albumId);

            if (isValid)
            {
                var updated = await service.Update(payload.WithId(albumId));

                return Accepted(mapper.Map<SingleResponse<AlbumModel>>(updated));
            }
            else{
                return NotFound();
            }
        }

        /// <summary>
        /// Remove existing album
        /// </summary>
        /// <param name="albumId">The album id</param>
        /// <returns></returns>
        [HttpDelete("{albumId}")]
        [SwaggerOperation(
            Description = "Delete album description goes here ", 
            OperationId = "AlbumDelete")]
        [SwaggerResponse((int)HttpStatusCode.Accepted)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(string albumId)
        {
            var isValid = await service.IsValidAsync(albumId);

            if (isValid)
            {
                await service.Delete(albumId);

                return Accepted();
            }
            else{
                return NotFound();
            }
        }
    }
}