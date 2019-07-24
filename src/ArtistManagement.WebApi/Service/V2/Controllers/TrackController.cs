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

namespace ArtistManagement.WebApi.V2.Controllers
{
    /// <summary>
    /// Track controller
    /// </summary>
    [ApiVersion("2"), Route("v{version:apiVersion}/tracks")]
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
        /// Remove existing track
        /// </summary>
        /// <param name="trackId">The track id</param>
        /// <returns></returns>
        [HttpDelete("{trackId}")]
        [SwaggerOperation(OperationId = "TrackDelete")]
        [SwaggerResponse((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> Delete(string trackId)
        {
            await track.Delete(trackId);

            return Accepted();
        }
    }
}