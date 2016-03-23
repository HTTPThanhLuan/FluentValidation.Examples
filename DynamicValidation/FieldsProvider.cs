using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model;
using DynamicValidation.Model.ValidationRule;

namespace DynamicValidation
{
    public static class FieldsProvider
    {
        public static IEnumerable<IField> GetFields()
        {
            return new IField[]
            {
                new Field<string>()
                {
                    Id = 1,
                    Name = "ISBN",
                    ValidationRules = new IValidationRule[]
                    {
                        new StringNotEmptyValidationRule(),
                        new StringRegexValidationRule(@"ISBN(-1(?:(0)|3))?:?\x20+(?(1)(?(2)(?:(?=.{13}$)\d{1,5}([ -])\d{1,7}\3\d{1,6}\3(?:\d|x)$)|(?:(?=.{17}$)97(?:8|9)([ -])\d{1,5}\4\d{1,7}\4\d{1,6}\4\d$))|(?(.{13}$)(?:\d{1,5}([ -])\d{1,7}\5\d{1,6}\5(?:\d|x)$)|(?:(?=.{17}$)97(?:8|9)([ -])\d{1,5}\6\d{1,7}\6\d{1,6}\6\d$)))"),
                    }
                },
                new Field<int>()
                {
                    Id = 2,
                    Name = "Fineness ",
                    ValidationRules = new IValidationRule[]
                    {
                        new IntegerRangeValidationRule(0, 1000)
                    }
                }
            };
        } 
    }
}
