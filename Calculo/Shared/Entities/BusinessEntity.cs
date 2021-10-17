using Spark.Core.Shared.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calculo.Shared.Entities
{
    public class BusinessEntity : IWithID<int>
    {
        [Required] public string Name { get; set; }
        
        public int ID { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }

        public ICollection<Operation> Operations { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is BusinessEntity entity)
                return ID == entity.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
