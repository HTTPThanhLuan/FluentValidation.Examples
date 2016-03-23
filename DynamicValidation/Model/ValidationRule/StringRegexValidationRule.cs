namespace DynamicValidation.Model.ValidationRule
{
    public class StringRegexValidationRule : IValidationRule
    {
        public StringRegexValidationRule(string regex)
        {
            Regex = regex;
        }

        public string Regex { get; private set; }
    }
}
