using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicValidation.Model;
using DynamicValidation.Validator;
using FluentValidation;
using FluentValidation.Results;

namespace DynamicValidation
{
    class Program
    {

        static void Main(string[] args)
        {
            var products = new Product[]
            {
                new Product()
                {
                    Name = "Book with wrong ISBN",
                    FieldValues = new FieldValue[]
                    {
                        new StringFieldValue() {FieldId = 1, Value = "12345"}
                    }
                },
                new Product()
                {
                    Name = "Jewelry with wrong fineness",
                    FieldValues = new FieldValue[]
                    {
                        new IntegerFieldValue() {FieldId = 2, Value = 1500 }
                    }
                }
            };

            foreach (var product in products)
            {
               // var validator = new ProductValidator();
               // var result = validator.Validate(product);
               // WriteValidationResults(product, result);
            }




            System.Console.ReadKey();
        }

        static void WriteValidationResults(Product product, ValidationResult result)
        {
            if (result.IsValid)
            {
                Console.WriteLine($"Validation for product {product.Name} succeeded.");
            }
            else
            {
                Console.WriteLine($"Validation for product {product.Name} failed.");
                foreach (var error in result.Errors)
                {
                    System.Console.WriteLine(error.ErrorMessage);
                }
            }
        }
    }
}
