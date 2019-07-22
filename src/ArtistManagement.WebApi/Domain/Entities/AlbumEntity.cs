using System;
using System.Collections.Generic;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Domain.Entities
{
    public class AlbumEntity : Entity
    {
        #region Constructor
        protected AlbumEntity()
        { 

        }

        public AlbumEntity(string artistId, string title, DateTime release)
        { 
            ArtistId = artistId;
            Name = title;
            Release = release;
        }
        #endregion

        #region Properties
        public string Id { get; private set; }
        public string ArtistId { get; private set; }
        public string Name { get; private set; }
        public DateTime Release { get; private set; }
        #endregion

        #region Artist
        public ArtistEntity Artist { get; private set; }
        #endregion

        #region Tracks
        public ICollection<AlbumTrackEntity> Tracks { get; private set; }

        public AlbumTrackEntity AddTrack(string trackId)
        {
            var albumTrack = new AlbumTrackEntity(
                albumId: Id,
                trackId: trackId
            );

            return albumTrack;
        }
        #endregion
    }
}