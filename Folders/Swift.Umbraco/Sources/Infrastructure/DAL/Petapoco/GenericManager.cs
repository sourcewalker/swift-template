using Swift.Umbraco.Business.Manager.Interfaces;
using Swift.Umbraco.Models.Domain;
using Swift.Umbraco.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Swift.Umbraco.Infrastructure.$safeprojectname$.Petapoco
{
    public class GenericManager<TEntityType> : IGenericManager<TEntityType> where TEntityType : EntityBase
    {
        protected readonly UmbracoDatabase _database;
        protected readonly ISqlSyntaxProvider _sqlProvider;

        public GenericManager()
        {
            _database = ApplicationContext.Current.DatabaseContext.Database;
            _sqlProvider = ApplicationContext.Current.DatabaseContext.SqlSyntax;
        }

        public IEnumerable<TEntityType> GetAll()
        {
            var query = new Sql()
                            .Select("*")
                            .From<TEntityType>(_sqlProvider);
            return _database.Fetch<TEntityType>(query);
        }

        public TEntityType GetById(Guid id)
        {
            var query = new Sql()
                            .Select("*")
                            .From<TEntityType>(_sqlProvider)
                            .Where<TEntityType>(cons => cons.Id == id, _sqlProvider);
            return _database.Fetch<TEntityType>(query).FirstOrDefault();
        }

        public Guid Create(TEntityType entity)
        {
            var id = Guid.NewGuid();
            entity.Id = id;
            entity.CreatedOn = DateTimeOffset.UtcNow.ToUtc();
            _database.Insert(entity);
            return id;
        }

        public bool Update(TEntityType entity)
        {
            if (entity.Id == default)
                return false;
            entity.UpdatedOn = DateTime.UtcNow;
            return _database.Update(entity) > 0;
        }

        public bool Delete(TEntityType entity)
        {
            return _database.Delete(entity) > 0;
        }
    }
}
