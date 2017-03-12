using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Search4Self.DAL.Models
{
    [Table("Searches")]
    public class SearchEntity : BaseEntity
    {
        public string Query { get; set; }

        public int Count { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}