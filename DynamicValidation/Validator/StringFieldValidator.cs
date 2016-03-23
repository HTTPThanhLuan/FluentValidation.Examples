using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model;
using DynamicValidation.Model.ValidationRule;
using FluentValidation;
using IValidationRule = DynamicValidation.Model.ValidationRule.IValidationRule;

namespace DynamicValidation.Validator
{
    public class StringFieldValidator : AbstractValidator<string>, IFieldValidator
    {
        private Dictionary<Type, Action<IValidationRule, IField>> _rulesDictionary;

        public StringFieldValidator(Field<string> field)
        {
            CreateDictionary();
            AttachValidators(field);
        }

        private void AttachValidators(Field<string> field)
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
                    [typeof (StringNotEmptyValidationRule)] = AddStringNotEmptyValidationRule,
                    [typeof (StringRegexValidationRule)] = AddStringStringRegexValidationRule
                };
        }

        private void AddStringNotEmptyValidationRule(IValidationRule validationRule, IField field)
        {
            this.RuleFor(s => s).NotEmpty().WithName(field.Name);
        }

        private void AddStringStringRegexValidationRule(IValidationRule validationRule, IField field)
        {
            var regexValidationRule = (StringRegexValidationRule) validationRule;
            this.RuleFor(s => s).Matches(regexValidationRule.Regex).WithName(field.Name);
        }
    }
}
