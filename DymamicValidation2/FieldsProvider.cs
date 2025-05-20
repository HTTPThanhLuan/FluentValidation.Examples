using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model;

using RulesEngine.Models;

namespace DynamicValidation
{
    public static class FieldsProvider
    {
        public static IEnumerable<Field> GetFields()
        {
            return new Field[]
            {
                new Field()
                {
                    Id = 1,
                    Name = "ISBN",
                    Type = typeof(string),
                    ValidationRules = new Rule[]
                    {
                       
                            new Rule()
                            {
                                RuleName = "ISBN",
                                SuccessEvent = "Count is within tolerance.",
                                ErrorMessage = "Over expected.",
                                Expression = "ISBN.Count() < 3",
                                RuleExpressionType = RuleExpressionType.LambdaExpression
                            } 

                    }
                },
                new Field()
                {
                    Id = 2,
                    Name = "Fineness",
                    Type =typeof(int),
                    ValidationRules = new Rule[]
                    {

                            new Rule()
                            {
                                RuleName = "Fineness",
                                SuccessEvent = "Count is within tolerance.",
                                ErrorMessage = "Over expected.",
                                Expression = "Fineness < 3",
                                RuleExpressionType = RuleExpressionType.LambdaExpression
                            }

                    }
                }
            };
        }
    }
}
