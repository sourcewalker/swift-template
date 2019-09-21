using Core.Infrastructure.Interfaces.DAL;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace $safeprojectname$.Repository.Base
{
    public abstract class RepositoryBase<TEntity> : IDisposable, IRepository<TEntity, Guid> 
        where TEntity : EntityBase<Guid>, new()
    {
        bool _disposed = false;

        protected readonly DatabaseContext Db;

        protected DbSet<TEntity> Table;

        protected RepositoryBase()
        {
            Db = new DatabaseContext();
            Table = Db.Set<TEntity>();
        }
        protected RepositoryBase(string connectionStringName)
        {
            Db = new DatabaseContext(connectionStringName);
            Table = Db.Set<TEntity>();
        }
        
        public DatabaseContext Context => Db;

        public bool HasChanges => Db.ChangeTracker.HasChanges();

        public int Count => Table.Count();

        public TEntity GetFirst() => Table.FirstOrDefault();

        public TEntity Find(Guid? id) => Table.Find(id);

        public virtual IEnumerable<TEntity> GetAll() => Table;

        internal IEnumerable<TEntity> GetRange(IQueryable<TEntity> query, int skip, int take)
            => query.Skip(skip).Take(take);
        public virtual IEnumerable<TEntity> GetRange(int skip, int take)
            => GetRange(Table, skip, take);

        public virtual int Add(TEntity entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int AddRange(IEnumerable<TEntity> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }
        public virtual int Update(TEntity entity, bool persist = true)
        {
            //Table.Attach(entity);
            Db.Entry(entity).State = EntityState.Modified;
            return persist ? SaveChanges() : 0;
        }
        public virtual int UpdateRange(IEnumerable<TEntity> entities, bool persist = true)
        {
            foreach (var entity in entities)
            {
                //Table.Attach(entity);
                Db.Entry(entity).State = EntityState.Modified;
            }
            return persist ? SaveChanges() : 0;
        }
        public virtual int Delete(TEntity entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }
        public virtual int DeleteRange(IEnumerable<TEntity> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        internal TEntity GetEntryFromChangeTracker(Guid? id)
        {
           return Db.ChangeTracker.Entries()
                    .Select((DbEntityEntry entity) => (TEntity)entity.Entity)
                    .FirstOrDefault(x => x.Id == id);
        }

        //TODO: Check For Cascade Delete
        public int Delete(Guid id, byte[] timeStamp, bool persist = true)
        {
            var entry = GetEntryFromChangeTracker(id);
            if (entry != null)
            {
                if (entry.TimeStamp.SequenceEqual(timeStamp))
                {
                    return Delete(entry, persist);
                }
                throw new Exception("Unable to delete due to concurrency violation.");
            }
            Db.Entry(new TEntity { Id = id, TimeStamp = timeStamp }).State = EntityState.Deleted;
            return persist ? SaveChanges() : 0;
        }

        public int SaveChanges()
        {
            try
            {
                return Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception($"{ex.HResult}");
            }
        }

        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            Db.Dispose();
            _disposed = true;
        }
    }
}
