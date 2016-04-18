using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace Web
{
public class ModelStateFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(HttpActionContext actionContext)
    {
        if (!actionContext.ModelState.IsValid)
        {
            var errors = actionContext.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.BadRequest, errors);
        }
    }
}
}