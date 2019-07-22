using System;
using System.Collections.Generic;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Domain.Entities
{
    public class ArtistEntity : Entity, IAggregateRoot
    {
        #region Constructor
            
        #endregion

        #region Properties
        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Nationality { get; private set; }

        public string Genre { get; private set; }
        #endregion

        #region Tracks
        public ICollection<TrackEntity> Tracks { get; private set; }

        public TrackEntity AddTrack(string title, DateTime release)
        {
            var track = new TrackEntity(
                artistId: Id,
                title: title
            );

            return track; 
        }
        #endregion

        #region Albums
        public ICollection<AlbumEntity> Albums { get; private set; }

        public AlbumEntity AddAlbum(string title, DateTime release)
        {
            var album = new AlbumEntity(
                artistId: Id, 
                title: title, 
                release: release
            );

            return album; 
        }
        #endregion
    }
}