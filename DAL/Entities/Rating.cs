using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Rating : BaseEntity
    {
        public virtual Route Route { get; set; }
        public virtual User User { get; set; }
        public int Mark { get; set; }
    }
}
