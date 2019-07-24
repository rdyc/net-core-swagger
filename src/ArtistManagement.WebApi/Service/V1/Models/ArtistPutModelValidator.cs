using System;
using ArtistManagement.WebApi.Application.Services;
using FluentValidation;

namespace ArtistManagement.WebApi.V1.Models
{
    internal class ArtistPutModelValidator : AbstractValidator<ArtistPutModel>
    {
        private readonly IArtistService artist;
        
        public ArtistPutModelValidator(IArtistService artist)
        {
            this.artist = artist ?? throw new ArgumentNullException(nameof(artist));

            // RuleFor(s => s.Id)
            //     .Must(BeValidArtist)
            //     .WithMessage("Unregistered artist id.");

            RuleFor(s => s.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(255);

            RuleFor(s => s.Nationality)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);
        }

        private bool BeValidArtist(string id)
        {
            return artist.IsValidAsync(id).Result;
        }
    }
}