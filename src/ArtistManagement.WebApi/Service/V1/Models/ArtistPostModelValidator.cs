using FluentValidation;

namespace ArtistManagement.WebApi.V1.Models
{
    internal class ArtistPostModelValidator : AbstractValidator<ArtistPostModel>
    {
        public ArtistPostModelValidator()
        {
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
    }
}