using System;
using ArtistManagement.WebApi.Application.Services;
using FluentValidation;

namespace ArtistManagement.WebApi.V1.Models
{
    internal class TrackPostModelValidator : AbstractValidator<TrackPostModel>
    {
        private readonly IArtistService artist;
        
        public TrackPostModelValidator(IArtistService artist)
        {
            this.artist = artist ?? throw new ArgumentNullException(nameof(artist));

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
    }
}