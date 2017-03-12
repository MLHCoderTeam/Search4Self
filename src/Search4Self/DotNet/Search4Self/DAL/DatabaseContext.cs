using Search4Self.DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Search4Self.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base(ConfigurationManager.ConnectionStrings["Search4Self"].ConnectionString)
        {
            Database.SetInitializer(new DatabaseInitializer());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MusicGenreEntity> MusicGenres { get; set; }
        public DbSet<YoutubeSearchHistoryEntity> YoutubeSearchHistory { get; set; }
        public DbSet<SearchEntity> Searches { get; set; }
    }
}