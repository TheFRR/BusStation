using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.Models
{
    public class FlightModel
    {
        public int RouteNumber { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public int SeatsNumber { get; set; }
        public int BusySeatsNumber { get; set; }
    }
}
