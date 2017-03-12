using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Search4Self.Controllers
{
    [RoutePrefix("api/google")]
    public class GoogleController : AuthInjectedController
    {
        [Route("searches")]
        [HttpGet]
        public IHttpActionResult Searches()
        {
            Authorize();

            var searches = User.Searches.OrderByDescending(s => s.Count).Take(20).Select(s => new { Word = s.Query, Count = s.Count }).ToList();

            return Ok(searches);
        }
    }
}