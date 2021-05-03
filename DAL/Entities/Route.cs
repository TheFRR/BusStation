using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Route : BaseEntity
    {
        public int Number { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
    }
}
