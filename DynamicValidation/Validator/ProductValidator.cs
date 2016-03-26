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
    public class ProductValidator: AbstractValidator<Product>
    {
        private IEnumerable<Field> _fields;
        private FieldValidatorFactory _fieldValidatorFactory;

        public ProductValidator()
        {
            _fields = FieldsProvider.GetFields();
            _fieldValidatorFactory = new FieldValidatorFactory();

            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.FieldValues).SetValidator(new FieldValuesValidator(_fields, _fieldValidatorFactory));
        }

        public override ValidationResult Validate(Product instance)
        {
            return base.Validate(instance);
        }
    }

    public class FieldValuesValidator : AbstractValidator<IEnumerable<FieldValue>>
    {
        private IEnumerable<Field> _fields;
        private FieldValidatorFactory _fieldValidatorFactory;

        public FieldValuesValidator(IEnumerable<Field> fields, FieldValidatorFactory fieldValidatorFactory)
        {
            _fields = fields;
            _fieldValidatorFactory = fieldValidatorFactory;
        }

        public override ValidationResult Validate(ValidationContext<IEnumerable<FieldValue>> context)
        {
            var fieldValues = context.InstanceToValidate;
            var validationResultList = new List<ValidationResult>();

            foreach (var fieldValue in fieldValues)
            {
                var field = _fields.First(f => f.Id == fieldValue.FieldId);
                var validationResult = _fieldValidatorFactory.RunFieldValidatorForField(field, fieldValue);
                validationResultList.Add(validationResult);
            }

            var errors = validationResultList.SelectMany(el => el.Errors);
            return new ValidationResult(errors);
        }
    }
}
