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

            var groupData = User.MusicGenres.GroupBy(m => m.Date.AddDays(-(int)m.Date.DayOfWeek)).ToList();

            var result = new BOL.MusicGenres()
            {
                Data = new List<Tuple<DateTime, List<Tuple<string, int>>>>(),
                Genres = User.MusicGenres.Select(m => m.Genre).Distinct().ToList()
            };

            foreach(var group in groupData)
            {
                var element = new Tuple<DateTime, List<Tuple<string, int>>>(group.Key, new List<Tuple<string, int>>());
                foreach (var genreGroup in group.GroupBy(g => g.Genre))
                {
                    element.Item2.Add(new Tuple<string, int>(genreGroup.Key, genreGroup.Select(g => g.Hits).DefaultIfEmpty(0).Sum()));
                }
            }

            return Ok(result);
        }

        [Route("words")]
        [HttpGet]
        public IHttpActionResult Words()
        {
            Authorize();

            var result = User.YoutubeSearches.OrderByDescending(y=>y.Count).Take(15).ToList();

            return Ok(result);
        }

    }
}