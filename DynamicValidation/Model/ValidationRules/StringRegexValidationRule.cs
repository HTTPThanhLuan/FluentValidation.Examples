namespace DynamicValidation.Model.ValidationRules
{
public class StringRegexValidationRule : ValidationRule
{
    public StringRegexValidationRule(string regex)
    {
        Regex = regex;
    }

    public string Regex { get; private set; }
}
}
