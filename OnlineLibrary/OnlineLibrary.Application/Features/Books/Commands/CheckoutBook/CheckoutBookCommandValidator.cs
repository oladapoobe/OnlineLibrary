using FluentValidation;

namespace OnlineLibrary.Application.Features.Books.Commands.CheckoutBook
{
    public class CheckoutBookCommandValidator : AbstractValidator<CheckoutBookCommand>
    {
        public CheckoutBookCommandValidator()
        {
            RuleFor(p => p.Name)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull()
              .MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.")
              .Matches("^[a-zA-Z0-9 .-]*$").WithMessage("{PropertyName} can only contain alphanumeric characters, spaces, dots, and dashes.");

            RuleFor(p => p.Author)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.")
                .Matches("^[a-zA-Z0-9 .-]*$").WithMessage("{PropertyName} can only contain alphanumeric characters, spaces, dots, and dashes.");

            RuleFor(p => p.Publisher)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(150).WithMessage("{PropertyName} must not exceed 150 characters.")
                .Matches("^[a-zA-Z0-9 .-]*$").WithMessage("{PropertyName} can only contain alphanumeric characters, spaces, dots, and dashes.");

        }
    }
}
