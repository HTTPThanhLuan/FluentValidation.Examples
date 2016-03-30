using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model;
using DynamicValidation.Model.ValidationRules;
using FluentValidation;

namespace DynamicValidation.Validator
{
public class IntegerFieldValidator : BaseFieldValidator<int>
{
    public IntegerFieldValidator(Field field) : base(field) { }

    private Dictionary<Type, Action<ValidationRule, Field>> ruleDictionary;

    protected override Dictionary<Type, Action<ValidationRule, Field>> RuleDictionary
    {
        get
        {
            return ruleDictionary ?? (ruleDictionary = new Dictionary<Type, Action<ValidationRule, Field>>
            {
                [typeof(IntegerRangeValidationRule)] = AddIntegerRangeValidationRule
            });
        }
    }

    private void AddIntegerRangeValidationRule(ValidationRule validationRule, Field field)
    {
        var rangeValidationRule = (IntegerRangeValidationRule) validationRule;

        this.RuleFor(i => i)
            .GreaterThan(rangeValidationRule.Min)
            .LessThan(rangeValidationRule.Max)
            .WithName(field.Name);
    }
}
}
