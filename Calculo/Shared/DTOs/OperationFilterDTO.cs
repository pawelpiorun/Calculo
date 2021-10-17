using Spark.Core.Shared.DTOs;
using System.Text.Json.Serialization;

namespace Calculo.Shared.DTOs
{
    public class OperationFilterDTO : EntityFilterDTO<OperationFilter>
    {
        public OperationFilterDTO()
        {
            Filter = new OperationFilter();
        }

        [JsonIgnore]
        public string Title
        {
            get => Filter.Title;
            set => Filter.Title = value;
        }
        [JsonIgnore]
        public int CategoryID
        {
            get => Filter.CategoryID;
            set => Filter.CategoryID = value;
        }
        [JsonIgnore]
        public bool Expense
        {
            get => Filter.Expense;
            set => Filter.Expense = value;
        }
        [JsonIgnore]
        public bool Income
        {
            get => Filter.Income;
            set => Filter.Income = value;
        }
    }

    public class OperationFilter
    {
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public bool Expense { get; set; } = true;
        public bool Income { get; set; } = true;
    }
}
