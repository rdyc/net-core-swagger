using System;
using System.Collections.Generic;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Domain.Entities
{
    public class AlbumEntity : Entity, IAggregateRoot
    {
        #region Constructor
        protected AlbumEntity()
        { 

        }

        public AlbumEntity(string title, DateTime release)
        { 
            Name = title;
            Release = release;
        }
        #endregion

        #region Properties
        public string Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Release { get; private set; }
        #endregion

        #region Tracks
        public ICollection<AlbumTrackEntity> AlbumTracks { get; private set; }

        public AlbumTrackEntity AddTrack(string trackId)
        {
            var albumTrack = new AlbumTrackEntity(
                albumId: Id,
                trackId: trackId
            );

            return albumTrack;
        }
        #endregion

        #region Methods

        public void SetUpdate(string name, DateTime release)
        {
            Name = name;
            Release = release;
        }
            
        #endregion
    }
}