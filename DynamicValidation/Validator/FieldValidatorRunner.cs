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
public class FieldValidatorRunner
{
    private Dictionary<Type, Func<Field, FieldValue, ValidationResult>> _fieldValidators;

    public FieldValidatorRunner()
    {
        _fieldValidators =
        new Dictionary<Type, Func<Field, FieldValue, ValidationResult>>()
        {
            [typeof(IntegerField)] = RunIntegerValidator,
            [typeof(StringField)] = RunStringValidator
        };
    }

    private ValidationResult RunIntegerValidator(Field field, FieldValue fieldValue)
    {
        var integerField = (IntegerField)field;
        var integerFieldValue = (IntegerFieldValue)fieldValue;
        var integerValidator = new IntegerFieldValidator(integerField);

        var validationResult = integerValidator.Validate(integerFieldValue.Value);

        return validationResult;
    }

    private ValidationResult RunStringValidator(Field field, FieldValue fieldValue)
    {
        var stringField = (StringField) field;
        var stringFieldValue = (StringFieldValue) fieldValue;
        var integerValidator = new StringFieldValidator(stringField);

        var validationResult = integerValidator.Validate(stringFieldValue.Value);

        return validationResult;
    }

    public ValidationResult RunFieldValidatorForField(Field field, FieldValue fieldValue)
    {
        var fieldValidatorRunner = _fieldValidators[field.GetType()];

        var validationResult = fieldValidatorRunner(field, fieldValue);

        return validationResult;
    }
}
}
