using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Search4Self.DAL.Models
{
    [Table("Users")]
    public class UserEntity : BaseEntity
    {

        [Index]
        public Guid Session { get; set; }

        public virtual List<MusicGenreEntity> MusicGenres { get; set; } = new List<MusicGenreEntity>();
        public virtual List<YoutubeSearchHistoryEntity> YoutubeSearches { get; set; } = new List<YoutubeSearchHistoryEntity>();
        public virtual List<SearchEntity> Searches { get; set; } = new List<SearchEntity>();

    }
}