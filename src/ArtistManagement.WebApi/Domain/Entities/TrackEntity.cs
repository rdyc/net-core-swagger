using System;
using System.Collections.Generic;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Domain.Entities
{
    public class TrackEntity : Entity
    {
        #region Constructor
        protected TrackEntity()
        { 

        }

        public TrackEntity(string artistId, string title)
        {
            Id = Guid.NewGuid().ToString();
            ArtistId = artistId;
            Title = title;
        }
        #endregion

        #region Properties
        public string Id { get; private set; }

        public string ArtistId { get; private set; }
        
        public string Title { get; private set; }
        #endregion

        #region Artist
        public ArtistEntity Artist { get; private set; }
        #endregion

        #region Album Tracks
        public ICollection<AlbumTrackEntity> AlbumTracks { get; private set; }
        #endregion
    }
}