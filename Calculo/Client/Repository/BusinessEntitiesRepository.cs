using Calculo.Client.Interfaces;
using Calculo.Shared.Entities;
using Spark.Core.Client.Repository;
using Spark.Core.Client.Services;
using Spark.Core.Shared.DTOs;
using System;
using System.Threading.Tasks;

namespace Calculo.Client.Repository
{
    public class BusinessEntitiesRepository : Repository<BusinessEntity, int>, IBusinessEntitiesRepository
    {
        protected override string Url => "/api/businessentities";

        public BusinessEntitiesRepository(IHttpService httpService)
            : base(httpService)
        {

        }

        public async Task<RatedEntityDTO<BusinessEntity>> GetRatedEntryAsync(int id)
        {
            var response = await httpService.Get<RatedEntityDTO<BusinessEntity>>($"{Url}/{id}/rated");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        //public override List<BusinessEntity> GetEntries()
        //{
        //    return null;
        //}
    }
}
