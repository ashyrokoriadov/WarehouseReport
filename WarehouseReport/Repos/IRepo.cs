using System.Collections.Generic;
using WarehouseReport.Models;

namespace WarehouseReport.Repos
{
    public interface IRepo<TEntity, in TEntitySource>
    {
        bool Add(TEntity entity);

        bool Exists(string entityName);

        IEnumerable<TEntity> GetAll();

        void Upsert(string entityName, TEntitySource subEntity);
    }
}
