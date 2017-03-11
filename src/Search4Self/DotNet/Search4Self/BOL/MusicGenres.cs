using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Search4Self.BOL
{
    public class MusicGenres
    {
        public List<string> Genres { get; set; } = new List<string>();

        public List<Tuple<DateTime, List<Tuple<string, int>>>> Data { get; set; } = new List<Tuple<DateTime, List<Tuple<string, int>>>>();
        //public List<DAL.Models.MusicGenreEntity> Data { get; set; } = new List<DAL.Models.MusicGenreEntity>();
    }
}