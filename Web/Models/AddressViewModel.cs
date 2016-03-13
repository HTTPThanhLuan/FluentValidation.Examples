using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;

namespace Web.Models
{
    [Validator(typeof(AddressViewModelValidator))]
    public class AddressViewModel
    {
        public string City { get; set; }

        public string PostalCode { get; set; }
    }

    public class AddressViewModelValidator : AbstractValidator<AddressViewModel>
    {
        public AddressViewModelValidator()
        {
            this.RuleFor(r => r.City).NotEmpty().Length(0, 50);

            this.RuleFor(r => r.PostalCode).NotEmpty().Matches("^[0-9]{2}-[0-9]{3}$");
        }
    }
}