using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Console
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            this.RuleFor(r => r.UserName).NotEmpty().Length(0, 50);

            this.RuleFor(r => r.Email).NotEmpty().EmailAddress().Length(0, 100);

            this.RuleFor(r => r.Password).NotEmpty().Length(6, 50);
        }
    }
}
