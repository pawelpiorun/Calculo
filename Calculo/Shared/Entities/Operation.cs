using Spark.Core.Shared.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Calculo.Shared.Entities
{
    public class Operation : IWithID<int>
    {
        [Required] public string Title { get; set; }
        [Required] public double Value { get; set; }
        [Required] public DateTime? Date { get; set; }
        [Required] public Category Category { get; set; }

        public int ID { get; set; }
        public string Description { get; set; }
        public BusinessEntity BusinessEntity { get; set; }

        public OperationType Type
        {
            get
            {
                if (Value >= 0)
                    return OperationType.Income;

                return OperationType.Expense;
            }
        }
    }

    public enum OperationType
    {
        Expense = 0,
        Income = 1
    }
}