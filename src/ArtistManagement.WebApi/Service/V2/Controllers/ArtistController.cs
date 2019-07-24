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
    /// Artist controller
    /// </summary>
    [ApiVersion("2"), Route("v{version:apiVersion}/artists")]
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
        /// Add new artist
        /// </summary>
        /// <param name="payload">The payload</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(OperationId = "ArtistPost")]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(SingleResponse<ArtistModel>))]
        public async Task<IActionResult> Post([FromBody]ArtistPostModel payload)
        {
            var created = await artist.Add(payload);

            return Created(string.Empty, mapper.Map<SingleResponse<ArtistModel>>(created));
        }
    }
}