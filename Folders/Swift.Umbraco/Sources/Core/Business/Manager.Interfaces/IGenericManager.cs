using System;
using System.Collections.Generic;

namespace Swift.Umbraco.$safeprojectname$.Manager.Interfaces
{
    public interface IGenericManager<TEntityType> where TEntityType : class
    {
        IEnumerable<TEntityType> GetAll();

        TEntityType GetById(Guid id);

        Guid Create(TEntityType consumer);

        bool Update(TEntityType consumer);

        bool Delete(TEntityType consumer);
    }
}
