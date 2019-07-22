using System;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Domain.Entities
{
    public class AlbumTrackEntity: Entity
    {
        #region Constructor
        protected AlbumTrackEntity()
        { 

        }

        public AlbumTrackEntity(string albumId, string trackId)
        {
            Id = Guid.NewGuid().ToString();
            AlbumId = albumId;
            TrackId = trackId;
        }
        #endregion

        #region Properties
        public string Id { get; private set; }

        public string TrackId { get; private set; }
        public string AlbumId { get; private set; }
        #endregion

        #region Album
        public AlbumEntity Album { get; private set; }
        #endregion

        #region Track
        public TrackEntity Track { get; private set; }
        #endregion
    }
}