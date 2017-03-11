using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Search4Self.DAL.Models
{
    [Table("MusicGenre")]
    public class MusicGenreEntity : BaseEntity
    {
        [Index]
        public Guid UserId { get; set; }

        public DateTime Date { get; set; }
        public string Genre { get; set; }
        public int Hits { get; set; }

        // Relations

        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
    }
}