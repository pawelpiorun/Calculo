using Calculo.Shared.Entities;
using Spark.Core.Client.Repository;

namespace Calculo.Client.Interfaces
{
    public interface IBusinessEntitiesRepository : IRepositoryWithRatings<BusinessEntity, int>
    {

    }
}
