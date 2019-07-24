using System;
using ArtistManagement.WebApi.Application.Services;
using FluentValidation;

namespace ArtistManagement.WebApi.V1.Models
{
    internal class TrackPutModelValidator : AbstractValidator<TrackPutModel>
    {
        private readonly IArtistService artist;
        private readonly ITrackService track;
        
        public TrackPutModelValidator(IArtistService artist, ITrackService track)
        {
            this.artist = artist ?? throw new ArgumentNullException(nameof(artist));
            this.track = track ?? throw new ArgumentNullException(nameof(track));

            // RuleFor(s => s.Id)
            //     .Must(BeValidTrack)
            //     .WithMessage("Unregistered track id.");

            RuleFor(s => s.ArtistId)
                .NotNull()
                .NotEmpty()
                .Must(BeValidArtist)
                .WithMessage("Unregistered artist id.");;

            RuleFor(s => s.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(s => s.Genre)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(s => s.Duration)
                .NotNull();
        }

        private bool BeValidArtist(string id)
        {
            return artist.IsValidAsync(id).Result;
        }

        private bool BeValidTrack(string id)
        {
            return track.IsValidAsync(id).Result;
        }
    }
}