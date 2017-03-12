using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Search4Self.DAL.Models
{
    [Table("YoutubeSearchHistory")]
    public class YoutubeSearchHistoryEntity : BaseEntity
    {
        public string Word { get; set; }

        public int Count { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}