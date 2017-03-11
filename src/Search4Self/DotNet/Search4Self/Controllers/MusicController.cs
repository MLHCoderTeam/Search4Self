using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Search4Self.Controllers
{
    [RoutePrefix("api/music")]
    public class MusicController : AuthInjectedController
    {
        [Route("Test")]
        [HttpGet]
        public IHttpActionResult Test()
        {
            Authorize();

            var v = new
            {
                Date = DateTime.Now,
                Number = 23,
                Text = "Acesta e un JSON fain."
            };

            return Ok(v);
        }


        [Route("genres")]
        [HttpGet]
        public IHttpActionResult Genres()
        {
            Authorize();


            return Ok();
        }

    }
}