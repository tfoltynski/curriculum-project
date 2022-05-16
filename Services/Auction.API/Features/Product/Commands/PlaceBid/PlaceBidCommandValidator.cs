using FluentValidation;

namespace Auction.API.Features.Product.Commands.PlaceBid
{
    public sealed class PlaceBidCommandValidator : AbstractValidator<PlaceBidCommand>
    {
        public PlaceBidCommandValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty().MinimumLength(5).MaximumLength(30);
            RuleFor(e => e.LastName).NotEmpty().MinimumLength(5).MaximumLength(25);
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.Phone).NotEmpty().Matches("^\\d{10}$").WithMessage("Phone must have 10 digits.");
        }
    }
}
