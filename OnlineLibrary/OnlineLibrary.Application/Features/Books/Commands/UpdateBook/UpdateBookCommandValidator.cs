using FluentValidation;

namespace OnlineLibrary.Application.Features.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{Name} is required.")
               .NotNull()
               .MaximumLength(150).WithMessage("{Name} must not exceed 150 characters.")
             .Matches("^[a-zA-Z0-9 .-]*$").WithMessage("{PropertyName} can only contain alphanumeric characters, spaces, dots, and dashes.");


            RuleFor(p => p.Author)
               .NotEmpty().WithMessage("{Author} is required.")
               .NotNull()
               .MaximumLength(150).WithMessage("{Author} must not exceed 150 characters.")
             .Matches("^[a-zA-Z0-9 .-]*$").WithMessage("{PropertyName} can only contain alphanumeric characters, spaces, dots, and dashes.");


            RuleFor(p => p.Publisher)
                .NotEmpty().WithMessage("{Publisher} is required.")
                .NotNull()
                .MaximumLength(150).WithMessage("{Publisher} must not exceed 150 characters.")
             .Matches("^[a-zA-Z0-9 .-]*$").WithMessage("{PropertyName} can only contain alphanumeric characters, spaces, dots, and dashes.");

        }
    }
}
