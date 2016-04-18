using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Web.Models;

namespace Web.Controllers
{
    public class UserController : ApiController
    {
        public HttpResponseMessage PostUser(UserViewModel user)
        {
            // create user

            return this.Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
