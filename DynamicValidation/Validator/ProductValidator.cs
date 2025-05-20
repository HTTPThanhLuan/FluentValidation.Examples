using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model;
using FluentValidation;
using FluentValidation.Results;

namespace DynamicValidation.Validator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.FieldValues).SetValidator(new FieldValuesValidator());
        }
    }
}
