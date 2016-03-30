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
public abstract class BaseFieldValidator<T> : AbstractValidator<T>
{
    protected BaseFieldValidator(Field field)
    {
        AttachValidators(field);
    }
    protected abstract Dictionary<Type, Action<ValidationRule, Field>> RuleDictionary { get; }

    private void AttachValidators(Field field)
    {
        foreach (var validationRule in field.ValidationRules)
        {
            var validationRuleAction = RuleDictionary[validationRule.GetType()];
            validationRuleAction(validationRule, field);
        }
    }
}
}
