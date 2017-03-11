using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Search4Self.Controllers
{
    [RoutePrefix("api/Default")]
    public class DefaultController : ApiController
    {
        [Route("Test")]
        [HttpGet]
        public IHttpActionResult Test()
        {
            var v = new
            {
                Date = DateTime.Now,
                Number = 23,
                Text = "Acesta e un JSON fain."
            };

            return Ok(v);
        }
    }
}