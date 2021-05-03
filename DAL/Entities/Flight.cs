using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Flight : BaseEntity
    {
        public virtual Route Route { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int BusySeatsNumber { get; set; }
        public int SeatsNumber { get; set; }
    }
}
