using System.Data.Entity;

namespace Search4Self.DAL
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            base.Seed(context);
        }
    }
}