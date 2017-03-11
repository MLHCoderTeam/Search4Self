using System.Data.Entity;
using Search4Self.DAL.Models;
using System;
using System.Linq;

namespace Search4Self.DAL.Repositories
{
    public class YoutubeSearchHistoryRepository : BaseRepository<YoutubeSearchHistoryEntity>
    {
        public YoutubeSearchHistoryRepository(DbContext context) : base(context)
        {
        }

        public void DeleteForUser(Guid userId)
        {
            _dbSet.RemoveRange(_dbSet.Where(y => y.UserId == userId));
        }
    }
}