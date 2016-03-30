using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model;
using DynamicValidation.Model.ValidationRules;
using FluentValidation;

namespace DynamicValidation.Validator
{
public class StringFieldValidator : BaseFieldValidator<string>
{
    public StringFieldValidator(Field field) : base(field) { }

    private Dictionary<Type, Action<ValidationRule, Field>> ruleDictionary;

    protected override Dictionary<Type, Action<ValidationRule, Field>> RuleDictionary
    {
        get
        {
            return ruleDictionary ?? (ruleDictionary =  new Dictionary<Type, Action<ValidationRule, Field>>
            {
                [typeof(StringNotEmptyValidationRule)] = AddStringNotEmptyValidationRule,
                [typeof(StringRegexValidationRule)] = AddStringStringRegexValidationRule
            });
        }
    }

    private void AddStringNotEmptyValidationRule(ValidationRule validationRule, Field field)
    {
        this.RuleFor(s => s).NotEmpty().WithName(field.Name);
    }

    private void AddStringStringRegexValidationRule(ValidationRule validationRule, Field field)
    {
        var regexValidationRule = (StringRegexValidationRule) validationRule;
        this.RuleFor(s => s).Matches(regexValidationRule.Regex).WithName(field.Name);
    }
}
}
