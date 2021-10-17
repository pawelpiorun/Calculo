using Calculo.Client.Interfaces;
using Calculo.Shared.Entities;
using Spark.Core.Client.Repository;
using Spark.Core.Client.Services;

namespace Calculo.Client.Repository
{
    public class CalculationsRepository : Repository<Calculation, int>, ICalculationsRepository
    {
        protected override string Url => "/api/calculations";

        public CalculationsRepository(IHttpService httpService)
            : base(httpService)
        {

        }


        //public override List<Calculation> GetEntries()
        //{
        //    return new List<Calculation>()
        //    {
        //        new Calculation() { ID = 1, DateTime = new DateTime(2001, 01, 05), Result = "Result 1" },
        //        new Calculation() { ID = 2, DateTime = new DateTime(2011, 05, 06), Result = "Result 2", },
        //        new Calculation() { ID = 3, DateTime = new DateTime(2021, 09, 07), Result = "Result 3" },
        //    };
        //}

    }
}
