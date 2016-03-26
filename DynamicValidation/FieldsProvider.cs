using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model;
using DynamicValidation.Model.ValidationRules;

namespace DynamicValidation
{
    public static class FieldsProvider
    {
        public static IEnumerable<Field> GetFields()
        {
            return new Field[]
            {
                new StringField() 
                {
                    Id = 1,
                    Name = "ISBN",
                    ValidationRules = new ValidationRule[]
                    {
                        new StringNotEmptyValidationRule(),
                        new StringRegexValidationRule(@"^(97(8|9))?\d{9}(\d|X)$"),
                    }
                },
                new IntegerField() 
                {
                    Id = 2,
                    Name = "Fineness ",
                    ValidationRules = new ValidationRule[]
                    {
                        new IntegerRangeValidationRule(0, 1000)
                    }
                }
            };
        } 
    }
}
