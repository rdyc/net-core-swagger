using System;
using ArtistManagement.WebApi.Application.Services;
using FluentValidation;

namespace ArtistManagement.WebApi.V1.Models
{
    internal class AlbumPostModelValidator : AbstractValidator<AlbumPostModel>
    {
        private readonly ITrackService track;
        
        public AlbumPostModelValidator(ITrackService track)
        {
            this.track = track ?? throw new ArgumentNullException(nameof(track));

            RuleFor(s => s.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(255);

            RuleFor(s => s.Release)
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now);

            RuleForEach(s => s.TrackIds)
                .NotNull()
                .Must(BeValidTrack)
                .WithMessage($"Unregistered track id.");
        }

        private bool BeValidTrack(string id)
        {
            return track.IsValidAsync(id).Result;
        }
    }
}