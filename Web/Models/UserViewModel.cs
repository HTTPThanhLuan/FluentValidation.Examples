using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;

namespace Web.Models
{
    [Validator(typeof(UserViewModelValidator))]
    public class UserViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public AddressViewModel Address { get; set; }
    }

    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            this.RuleFor(r => r.UserName).NotEmpty().Length(0, 50);

            this.RuleFor(r => r.Email).NotEmpty().EmailAddress().Length(0, 100);

            this.RuleFor(r => r.Password).NotEmpty().Length(6, 50);
        }
    }
}