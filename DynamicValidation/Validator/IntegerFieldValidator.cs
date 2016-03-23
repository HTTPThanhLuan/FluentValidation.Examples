using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model;
using DynamicValidation.Model.ValidationRule;
using FluentValidation;
using IValidationRule = DynamicValidation.Model.ValidationRule.IValidationRule;

namespace DynamicValidation.Validator
{
    public class IntegerFieldValidator : AbstractValidator<int>, IFieldValidator
    {
        private Dictionary<Type, Action<IValidationRule, IField>> _rulesDictionary;

        public IntegerFieldValidator(Field<int> field)
        {
            CreateDictionary();
            AttachValidators(field);
        }

        private void AttachValidators(Field<int> field)
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
                new Dictionary<Type, Action<IValidationRule, IField>>
                {
                    [typeof(StringNotEmptyValidationRule)] = AddIntegerRangeValidationRule,
                };
        }

        private void AddIntegerRangeValidationRule(IValidationRule validationRule, IField field)
        {
            var rangeValidationRule = (IntegerRangeValidationRule) validationRule;

            this.RuleFor(i => i)
                .GreaterThan(rangeValidationRule.Min)
                .LessThan(rangeValidationRule.Max)
                .WithName(field.Name);
        }
    }
}
