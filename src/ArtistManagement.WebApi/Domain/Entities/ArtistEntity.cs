using System;
using System.Collections.Generic;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Domain.Entities
{
    public class ArtistEntity : Entity, IAggregateRoot
    {
        #region Constructor
        protected ArtistEntity(){}

        public ArtistEntity(string name, string nationality)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Nationality = nationality;
        }

        #endregion

        #region Properties
        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Nationality { get; private set; }
        #endregion

        #region Tracks
        public ICollection<TrackEntity> Tracks { get; private set; }

        public TrackEntity AddTrack(string title, string genre, TimeSpan duration)
        {
            var track = new TrackEntity(
                artist: this,
                title: title,
                genre: genre,
                duration: duration
            );

            return track; 
        }
        #endregion
    
        #region Methods

        public void SetUpdate(string name, string nationality)
        {
            Name = name;
            Nationality = nationality;
        }
            
        #endregion
    }
}