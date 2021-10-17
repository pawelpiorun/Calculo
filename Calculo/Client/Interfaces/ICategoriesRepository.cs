using Calculo.Shared.Entities;
using Spark.Core.Client.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calculo.Client.Interfaces
{
    public interface ICategoriesRepository : IRepository<Category, int>
    {
        Task<List<Category>> GetCategoriesByNameAsync(string searchText);
    }
}
