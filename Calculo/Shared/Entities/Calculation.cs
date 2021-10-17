using Spark.Core.Shared.Interfaces;
using System;

namespace Calculo.Shared.Entities
{
    public class Calculation : IWithID<int>
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public string Result { get; set; }
        public string Expression { get; set; }
    }
}