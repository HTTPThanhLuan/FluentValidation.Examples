namespace DynamicValidation.Model.ValidationRule
{
    public class IntegerRangeValidationRule : IValidationRule
    {
        public IntegerRangeValidationRule(int min, int max)
        {
            Min = min;
            Max = max;
        }
        public int Min { get; private set; }
        public int Max { get; private set; }
    }
}
