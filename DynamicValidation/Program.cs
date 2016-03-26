using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model;
using DynamicValidation.Validator;
using FluentValidation.Results;

namespace DynamicValidation
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("heheczka");
            var product = new Product()
            {
                Name = "Implementing Domain-Driven Desing",
                FieldValues = new FieldValue[]
                {
                    new StringFieldValue() { FieldId = 1, Value = "9780133039900" }
                }
            };

            var validator = new ProductValidator();
            var result = validator.Validate(product);
            WriteValidationResults(result);

            System.Console.ReadKey();
        }

        static void WriteValidationResults(ValidationResult result)
        {
            foreach (var error in result.Errors)
            {
                System.Console.WriteLine(error.ErrorMessage);
            }
        }
    }
}
