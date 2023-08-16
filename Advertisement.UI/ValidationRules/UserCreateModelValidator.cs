using Advertisement.UI.Models;
using FluentValidation;

namespace Advertisement.UI.ValidationRules
{
    public class UserCreateModelValidator:AbstractValidator<UserCreateModel>
    {
        public UserCreateModelValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x=>x.Password).MinimumLength(3);
            RuleFor(x => x.Password).Equal(x=>x.ConfirmPassword).WithMessage("Password not match");
            RuleFor(x=>x.UserName).NotEmpty();
            RuleFor(x => new
            {
                x.UserName,
                x.FirstName
            }).Must(x =>  CannotFirstName(x.UserName,x.FirstName)).WithMessage("username contains firstname").When(x=>x.UserName!=null && x.FirstName !=null);
            RuleFor(x=>x.FirstName).NotEmpty();
            RuleFor(x=>x.Surname).NotEmpty();
        }

        private bool CannotFirstName(string userName , string firstName)
        {
            return !userName.Contains(firstName);
        }
    }
}
