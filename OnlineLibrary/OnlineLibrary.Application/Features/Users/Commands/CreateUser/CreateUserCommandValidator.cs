using FluentValidation;

namespace OnlineLibrary.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("{Username} is required.")
                .NotNull()
                .MaximumLength(150).WithMessage("{Username} must not exceed 150 characters.")
             .Matches("^[a-zA-Z0-9 .-]*$").WithMessage("{PropertyName} can only contain alphanumeric characters, spaces, dots, and dashes.");


            RuleFor(p => p.Password)
               .NotEmpty().WithMessage("{Password} is required.")
               .NotNull()
               .MaximumLength(150).WithMessage("{Password} must not exceed 150 characters.")
                .Matches("^[a-zA-Z0-9 .-]*$").WithMessage("{PropertyName} can only contain alphanumeric characters, spaces, dots, and dashes.");



        }
    }
}
