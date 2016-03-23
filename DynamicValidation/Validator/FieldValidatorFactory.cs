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
    public class FieldValidatorFactory
    {
        private Dictionary<Type, Func<IField, IFieldValue, ValidationResult>> _fieldValidators;

        public FieldValidatorFactory()
        {
            _fieldValidators =
            new Dictionary<Type, Func<IField, IFieldValue, ValidationResult>>()
            {
                [typeof(Field<int>)] = RunIntegerValidator,
                [typeof(Field<string>)] = RunStringValidator
            };
        }

        private ValidationResult RunIntegerValidator(IField field, IFieldValue fieldValue)
        {
            var integerField = (Field<int>)field;
            var integerFieldValue = (FieldValue<int>)fieldValue;
            var integerValidator = new IntegerFieldValidator(integerField);

            var validationResult = integerValidator.Validate(integerFieldValue.Value);

            return validationResult;
        }

        private ValidationResult RunStringValidator(IField field, IFieldValue fieldValue)
        {
            var stringField = (Field<string>) field;
            var stringFieldValue = (FieldValue<string>) fieldValue;
            var integerValidator = new StringFieldValidator(stringField);

            var validationResult = integerValidator.Validate(stringFieldValue.Value);

            return validationResult;
        }

        public ValidationResult RunFieldValidatorForField(IField field, IFieldValue fieldValue)
        {
            var fieldValidatorRunner = _fieldValidators[field.GetType()];

            var validationResult = fieldValidatorRunner(field, fieldValue);

            return validationResult;
        }
    }
}
