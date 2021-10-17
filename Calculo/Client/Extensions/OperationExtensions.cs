using Calculo.Shared.Entities;
using System;

namespace Calculo.Client.Extensions
{
    public static class OperationExtensions
    {
        public static string GetMarkupValue(this Operation operation)
        {
            var isExpense = operation.Type == OperationType.Expense;
            string color = isExpense ? "red" : "green";
            var value = string.Format("{0:0.00}", Math.Round(operation.Value, 2));
            var currency = "zł";
            return $"<text style=\"color: {color};\"> {value} {currency}</text>";
        }
    }
}
