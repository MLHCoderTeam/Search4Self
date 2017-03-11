using System.Data.Entity;
using Search4Self.DAL.Models;

namespace Search4Self.DAL.Repositories
{
    public class SearchesRepository : BaseRepository<SearchEntity>
    {
        public SearchesRepository(DbContext context) : base(context)
        {
        }
    }
}