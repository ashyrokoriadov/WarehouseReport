using System.Collections.Generic;

namespace WarehouseReport.Repos
{
    public interface IRepoFiller<in TEntitySource> 
    {
        void Fill(IEnumerable<TEntitySource> items);
    }
}
