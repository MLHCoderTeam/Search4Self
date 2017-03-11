using Search4Self.DAL.Models;
using System;
using System.Linq;

namespace Search4Self.DAL.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>
    {
        internal UserRepository(DatabaseContext context) : base(context)
        {
        }

        public UserEntity GetBySession(Guid session)
        {
            return _dbSet.FirstOrDefault(u => u.Session == session);
        }
    }
}