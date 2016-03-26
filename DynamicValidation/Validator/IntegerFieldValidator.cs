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
    public class IntegerFieldValidator : AbstractValidator<int>, IFieldValidator
    {
        private Dictionary<Type, Action<ValidationRule, IntegerField>> _rulesDictionary;

        public IntegerFieldValidator(IntegerField field)
        {
            CreateDictionary();
            AttachValidators(field);
        }

        private void AttachValidators(IntegerField field)
        {
            foreach (var validationRule in field.ValidationRules)
            {
                var validationRuleAction = _rulesDictionary[validationRule.GetType()];
                validationRuleAction(validationRule, field);
            }
        }

        private void CreateDictionary()
        {
            this._rulesDictionary =
                new Dictionary<Type, Action<ValidationRule, IntegerField>>
                {
                    [typeof(IntegerRangeValidationRule)] = AddIntegerRangeValidationRule,
                };
        }

        private void AddIntegerRangeValidationRule(ValidationRule validationRule, IntegerField field)
        {
            var rangeValidationRule = (IntegerRangeValidationRule) validationRule;

            this.RuleFor(i => i)
                .GreaterThan(rangeValidationRule.Min)
                .LessThan(rangeValidationRule.Max)
                .WithName(field.Name);
        }
    }
}
