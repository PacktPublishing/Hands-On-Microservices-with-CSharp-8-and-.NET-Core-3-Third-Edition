using FluentValidation;
namespace FlixOne.BookStore.UserService.Models.Validators
{
    public class RegisterUserViewModelValidator : AbstractValidator<RegisterUserViewModel>
    {
        public RegisterUserViewModelValidator()
        {
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("FirstName cannot be empty");
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("FirstName cannot be empty");
            RuleFor(r => r.EmailId).NotEmpty().WithMessage("EmailId cannot be empty");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(r => r.Password).Length(6, 12).WithMessage("Password must be between 6 and 12 characters");
            RuleFor(r => r.Mobile).NotEmpty().WithMessage("Mobile cannot be empty");
        }
    }
}
