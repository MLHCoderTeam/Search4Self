using Search4Self.DAL.Repositories;
using System;
using System.Data.Entity;

namespace Search4Self.DAL
{
    public class UnitOfWork : IDisposable
    {

        #region Properties and Constructors

        internal DatabaseContext _dbContext;

        public UnitOfWork() : this(new DatabaseContext()) { }

        public UnitOfWork(DatabaseContext context)
        {
            _dbContext = context;
            _disposed = false;
        }

        public DbContext Context
        {
            get
            {
                return _dbContext;
            }
        }

        #endregion

        #region Disposing logic

        private static bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing) return;

            _dbContext.Dispose();
            _disposed = true;
        }

        #endregion Disposing logic

        #region Repository fields

        private UserRepository _userRepository;
        private MusicGenreRepository _musicGenreRepository;
        private YoutubeSearchHistoryRepository _youtubeSearchHistoryRepository;

        #endregion

        #region Repository Properties

        public UserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_dbContext));
        public MusicGenreRepository MusicGenreRepository => _musicGenreRepository ?? (_musicGenreRepository = new MusicGenreRepository(_dbContext));
        public YoutubeSearchHistoryRepository YoutubeSearchHistoryRepository => _youtubeSearchHistoryRepository
            ?? (_youtubeSearchHistoryRepository = new YoutubeSearchHistoryRepository(_dbContext));

        #endregion
    }
}