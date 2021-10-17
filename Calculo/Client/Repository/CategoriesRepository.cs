using Calculo.Client.Interfaces;
using Calculo.Shared.Entities;
using Spark.Core.Client.Repository;
using Spark.Core.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculo.Client.Repository
{
    public class CategoriesRepository : Repository<Category, int>, ICategoriesRepository
    {
        protected override string Url => "/api/categories";

        public CategoriesRepository(IHttpService httpService)
            : base(httpService)
        {

        }


        public async Task<List<Category>> GetCategoriesByNameAsync(string searchText)
        {
            var response = await httpService.Get<List<Category>>(Url + $"/search/{searchText}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response.Where(x => x != null).ToList();
        }

        public async override Task<List<Category>> GetEntriesByAsync(string propertyName, string value)
        {
            if (string.Equals(propertyName, nameof(Category.Name)))
                return await GetCategoriesByNameAsync(value);

            return await base.GetEntriesByAsync(propertyName, value);
        }
    }
}
