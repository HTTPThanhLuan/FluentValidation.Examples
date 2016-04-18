using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using FluentValidation;

namespace Web.Models
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IContainer container;

        public AutofacValidatorFactory(IContainer container)
        {
            this.container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator = (IValidator)container.Resolve(validatorType);
            return validator;
        }
    }
}