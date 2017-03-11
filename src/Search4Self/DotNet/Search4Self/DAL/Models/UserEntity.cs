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

    }
}