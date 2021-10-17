using Calculo.Shared.DTOs;
using Calculo.Shared.Entities;
using Spark.Core.Client.Repository;
using System.Threading.Tasks;

namespace Calculo.Client.Interfaces
{
    public interface IOperationsRepository : IRepository<Operation, int>
    {
        Task<IncomesAndExpensesDTO> GetIncomesAndExpensesAsync();
    }
}
