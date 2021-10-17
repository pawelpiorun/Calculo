using Calculo.Client.Interfaces;
using Calculo.Shared.DTOs;
using Calculo.Shared.Entities;
using Spark.Core.Client.Repository;
using Spark.Core.Client.Services;
using System;
using System.Threading.Tasks;

namespace Calculo.Client.Repository
{
    public class OperationsRepository : Repository<Operation, int>, IOperationsRepository
    {
        protected override string Url => "/api/operations";

        public OperationsRepository(IHttpService httpService)
            : base(httpService)
        {

        }

        public async Task<IncomesAndExpensesDTO> GetIncomesAndExpensesAsync()
        {
            var response = await httpService.Get<IncomesAndExpensesDTO>(Url + "/iedto");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        //public override List<Operation> GetEntries()
        //{
        //    return new List<Operation>()
        //    {
        //        new Operation() { ID = 1, Date = new DateTime(2001, 01, 05), Title = "Czynsz", Category = new Category() { Name = "Mieszkanie", ID = 1 }, Value = -2150 },
        //        new Operation() { ID = 2, Date = new DateTime(2011, 05, 06), Title = "Pensja", Category = new Category() { Name = "Praca", ID = 2 }, Value = 5000 },
        //        new Operation() { ID = 3, Date = new DateTime(2021, 09, 07), Title = "Paliwo", Category = new Category() { Name = "Samochód", ID = 3 }, Value = -200}
        //    };
        //}
    }
}
