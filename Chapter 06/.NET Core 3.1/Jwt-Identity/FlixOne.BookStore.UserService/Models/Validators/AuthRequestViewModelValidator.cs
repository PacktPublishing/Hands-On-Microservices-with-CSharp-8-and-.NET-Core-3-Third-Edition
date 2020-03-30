using FluentValidation;
namespace FlixOne.BookStore.UserService.Models.Validators
{
    public class AuthRequestViewModelValidator : AbstractValidator<AuthRequestViewModel>
    {
        public AuthRequestViewModelValidator()
        {
            RuleFor(auth => auth.LoginId).NotEmpty().WithMessage("LoginId cannot be empty");
            RuleFor(auth => auth.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(auth => auth.Password).Length(6, 12).WithMessage("Password must be between 6 and 12 characters");
        }
    }
}
