using FluentValidation;
using SmartTaskApp.Auth.WebApi.Application.Commands.RegisterUser;

namespace SmartTaskApp.Auth.WebApi.Validations
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.Now);
        }
    }
}