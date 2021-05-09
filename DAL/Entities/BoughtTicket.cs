using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class BoughtTicket : BaseEntity
    {
        public virtual User User { get; set; }
        public virtual Ticket Ticket {get; set; }
        public bool IsPaid { get; set; }
    }
}
