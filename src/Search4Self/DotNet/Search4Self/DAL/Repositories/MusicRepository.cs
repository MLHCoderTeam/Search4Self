using Search4Self.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Search4Self.DAL.Repositories
{
    public class MusicGenreRepository : BaseRepository<MusicGenreEntity>
    {
        internal MusicGenreRepository(DatabaseContext context)
            : base(context)
        {
        }

        public void DeleteForUser(Guid userId)
        {
            _dbSet.RemoveRange(_dbSet.Where(y => y.UserId == userId));
        }
    }
}