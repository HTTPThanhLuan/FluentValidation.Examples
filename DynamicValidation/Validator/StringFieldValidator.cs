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
    public class StringFieldValidator : AbstractValidator<string>, IFieldValidator
    {
        private Dictionary<Type, Action<ValidationRule, StringField>> _rulesDictionary;

        public StringFieldValidator(StringField field)
        {
            CreateDictionary();
            AttachValidators(field);
        }

        private void AttachValidators(StringField field)
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
                new Dictionary<Type, Action<ValidationRule, StringField>>
                {
                    [typeof (StringNotEmptyValidationRule)] = AddStringNotEmptyValidationRule,
                    [typeof (StringRegexValidationRule)] = AddStringStringRegexValidationRule
                };
        }

        private void AddStringNotEmptyValidationRule(ValidationRule validationRule, StringField field)
        {
            this.RuleFor(s => s).NotEmpty().WithName(field.Name);
        }

        private void AddStringStringRegexValidationRule(ValidationRule validationRule, StringField field)
        {
            var regexValidationRule = (StringRegexValidationRule) validationRule;
            this.RuleFor(s => s).Matches(regexValidationRule.Regex).WithName(field.Name);
        }
    }
}
