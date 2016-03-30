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
public class FieldValuesValidator : AbstractValidator<IEnumerable<FieldValue>>
{
    private IEnumerable<Field> _fields;
    private FieldValidatorRunner _fieldValidationRunner;

    public FieldValuesValidator()
    {
        _fields = FieldsProvider.GetFields();
        _fieldValidationRunner = new FieldValidatorRunner();
    }

    public override ValidationResult Validate(ValidationContext<IEnumerable<FieldValue>> context)
    {
        var fieldValues = context.InstanceToValidate;
        var validationResultList = new List<ValidationResult>();

        foreach (var fieldValue in fieldValues)
        {
            var field = _fields.First(f => f.Id == fieldValue.FieldId);
            var validationResult = _fieldValidationRunner.RunFieldValidatorForField(field, fieldValue);
            validationResultList.Add(validationResult);
        }

        var errors = validationResultList.SelectMany(el => el.Errors);
        return new ValidationResult(errors);
    }
}
}
