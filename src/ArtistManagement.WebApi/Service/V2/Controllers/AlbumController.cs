using System;
using System.Net;
using System.Threading.Tasks;
using ArtistManagement.WebApi.Application.Responses;
using ArtistManagement.WebApi.Application.Services;
using ArtistManagement.WebApi.V1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ArtistManagement.WebApi.V2.Controllers
{
    /// <summary>
    /// Album controller
    /// </summary>
    [ApiVersion("2"), Route("v{version:apiVersion}/albums")]
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
        /// Get specific album
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        [HttpGet("{albumId}")]
        [SwaggerOperation(OperationId = "AlbumGetById")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(SingleResponse<AlbumModel>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string albumId)
        {
            var data = await service.Get(albumId);
            
            if (data == null)
                return NotFound();

            return Ok(mapper.Map<SingleResponse<AlbumModel>>(data));
        }
    }
}