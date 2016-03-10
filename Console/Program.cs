using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo ci = new CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var validator = new UserValidator();

            var usersToValidate = new User[]
            {
                new User() {Email = "user@gmail.com", Password = "123456", UserName = "user"},
                new User() {Email = "user@gmail", Password = "123", UserName = ""}
            };

            foreach (var user in usersToValidate)
            {
                var result = validator.Validate(user);
                WriteValidationResults(user, result);
            }

            System.Console.ReadKey();
        }

        static void WriteValidationResults(User user, ValidationResult result)
        {
            if (result.IsValid)
            {
                System.Console.WriteLine($"Walidacja dla użytkownika {user.UserName} powiodła się.");
            }
            else
            {
                System.Console.WriteLine($"Walidacja dla użytkownika {user.UserName} nie powiodła się.");
                foreach (var error in result.Errors)
                {
                    System.Console.WriteLine(error.ErrorMessage);
                }
            }
        }

    }
}
