using Search4Self.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Search4Self.DAL.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        internal readonly DbContext _dbContext;
        internal readonly DbSet<T> _dbSet;

        internal BaseRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        #region Persistence logic

        public bool Save()
        {
            try
            {
                _dbContext.SaveChanges();

                return true;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        #endregion

        #region CRUD

        public T Get(Guid id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T Insert(T obj)
        {
            if (obj == null)
                return null;

            if (obj.Id == null || obj.Id == Guid.Empty)
                obj.Id = GetNewId();

            _dbSet.Add(obj);

            return Save() ? _dbSet.Find(obj.Id) : null;
        }

        public T[] InsertAll(T[] objs)
        {
            if (objs == null || objs.Length == 0)
                return null;

            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].Id == null || objs[i].Id == Guid.Empty)
                    objs[i].Id = GetNewId();
            }

            _dbSet.AddRange(objs);

            return Save() ? objs : null;
        }

        public T Update(T obj)
        {
            if (obj == null)
                return null;

            return Save() ? _dbSet.Find(obj.Id) : null;
        }

        public bool Delete(Guid id)
        {
            return Delete(_dbSet.Find(id));
        }

        public bool Delete(T obj)
        {
            if (obj == null)
                return false;

            if (_dbContext.Entry(obj).State == EntityState.Detached)
                _dbSet.Attach(obj);

            _dbSet.Remove(obj);

            return Save();
        }

        public bool DeleteRange(T[] objs)
        {
            if (objs == null)
                return false;

            _dbSet.RemoveRange(objs);

            return Save();
        }

        #endregion

        #region ID handling

        public Guid GetNewId() => Guid.NewGuid();

        #endregion
    }
}