using Calculo.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo.Shared.DTOs
{
    public class IncomesAndExpensesDTO
    {
        public List<Operation> Incomes { get; set; }
        public List<Operation> Expenses { get; set; }
    }
}
