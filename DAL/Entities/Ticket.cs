using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Ticket : BaseEntity
    {
        public virtual Flight Flight { get; set; }
        public int Cost { get; set; }
    }
}
