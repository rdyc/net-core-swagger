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

        public TrackEntity(ArtistEntity artist, string title, string genre, TimeSpan duration)
        {
            Id = Guid.NewGuid().ToString();
            ArtistId = artist.Id;
            Artist = artist;
            Title = title;
            Genre = genre;
            Duration = duration;
        }
        #endregion

        #region Properties
        public string Id { get; private set; }

        public string ArtistId { get; private set; }
        
        public string Title { get; private set; }

        public string Genre { get; private set; }

        public TimeSpan Duration { get; private set; }
        #endregion

        #region Methods
        public void SetUpdate(string title, string genre, TimeSpan duration)
        {
            Title = title;
            Genre = genre;
            Duration = duration;
        }
        #endregion

        #region Artist
        public ArtistEntity Artist { get; private set; }
        #endregion

        #region Album Tracks
        public ICollection<AlbumTrackEntity> AlbumTracks { get; private set; }
        #endregion
    }
}