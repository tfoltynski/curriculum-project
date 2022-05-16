using FluentValidation;

namespace Auction.API.Features.Product.Commands.CreateProduct
{
    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(e => e.ProductName).NotEmpty().MinimumLength(5).MaximumLength(30);
            RuleFor(e => e.SellerInformation).NotNull().SetValidator(new SellerInformationDtoValidator());
        }

        public sealed class SellerInformationDtoValidator : AbstractValidator<CreateProductCommand.SellerInformationDto>
        {
            public SellerInformationDtoValidator()
            {
                RuleFor(e => e.FirstName).NotEmpty().MinimumLength(5).MaximumLength(30);
                RuleFor(e => e.LastName).NotEmpty().MinimumLength(5).MaximumLength(25);
                RuleFor(e => e.Email).NotEmpty().EmailAddress();
                RuleFor(e => e.Phone).NotEmpty().Matches("^\\d{10}$").WithMessage("Phone must have 10 digits.");
            }
        }
    }
}
