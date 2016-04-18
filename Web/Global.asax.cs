using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Features.Variance;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FluentValidation;
using FluentValidation.Mvc;
using FluentValidation.WebApi;
using Web.Models;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            var container = ConfigureAutofac();

            FluentValidation.Mvc.FluentValidationModelValidatorProvider.Configure(
                p => p.ValidatorFactory = new AutofacValidatorFactory(container));

            FluentValidation.WebApi.FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration, 
                p => p.ValidatorFactory = new AutofacValidatorFactory(container));
        }

        private IContainer ConfigureAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
                .ForEach(match =>
                {
                    builder.RegisterType(match.ValidatorType).As(match.InterfaceType);
                });

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));



            return container;
        }
    }
}
