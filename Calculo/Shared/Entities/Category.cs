using Spark.Core.Shared.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calculo.Shared.Entities
{
    public class Category : IWithID<int>
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }

        public ICollection<Operation> Operations { get; set; }
    }
}
