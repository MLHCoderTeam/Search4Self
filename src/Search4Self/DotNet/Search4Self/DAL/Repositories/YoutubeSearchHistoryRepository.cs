using System.Data.Entity;
using Search4Self.DAL.Models;

namespace Search4Self.DAL.Repositories
{
    public class YoutubeSearchHistoryRepository : BaseRepository<YoutubeSearchHistoryEntity>
    {
        public YoutubeSearchHistoryRepository(DbContext context) : base(context)
        {
        }
    }
}