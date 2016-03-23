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
        private IEnumerable<IField> _fields;
        private FieldValidatorFactory _fieldValidatorFactory;

        public ProductValidator()
        {
            _fields = FieldsProvider.GetFields();
            _fieldValidatorFactory = new FieldValidatorFactory();

            RuleFor(p => p.Name);
            Custom(p => ValidateFieldValues(p.FieldValues));
        }

        private ValidationFailure ValidateFieldValues(IEnumerable<IFieldValue> fieldValues)
        {
            var validationResultList = new List<ValidationResult>();

            foreach (var fieldValue in fieldValues)
            {
                var field = _fields.First(f => f.Id == fieldValue.FieldId);
                var validationResult = _fieldValidatorFactory.RunFieldValidatorForField(field, fieldValue);
                validationResultList.Add(validationResult);
            }

            return new ValidationResult();
        }
    }
}
