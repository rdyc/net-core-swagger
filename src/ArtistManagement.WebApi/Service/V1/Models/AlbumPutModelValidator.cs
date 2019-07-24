using System;
using ArtistManagement.WebApi.Application.Services;
using FluentValidation;

namespace ArtistManagement.WebApi.V1.Models
{
    internal class AlbumPutModelValidator : AbstractValidator<AlbumPutModel>
    {
        private readonly IAlbumService album;
        private readonly ITrackService track;
        
        public AlbumPutModelValidator(IAlbumService album, ITrackService track)
        {
            this.album = album ?? throw new ArgumentNullException(nameof(album));
            this.track = track ?? throw new ArgumentNullException(nameof(track));

            RuleFor(s => s.Id)
                .Must(BeValidAlbum)
                .WithMessage("Unregistered album id.");

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

        private bool BeValidAlbum(string id)
        {
            return album.IsValidAsync(id).Result;
        }

        private bool BeValidTrack(string id)
        {
            return track.IsValidAsync(id).Result;
        }
    }
}