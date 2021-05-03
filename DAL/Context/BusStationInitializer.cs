using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Context
{
    public static class BusStationInitializer
    {
        public static void Initialize(BaseContext baseContext)
        {
            baseContext.Database.EnsureCreated();

            if (baseContext.Route.Any())
            {
                return;
            }
            var routes = new Route[]
            {
                new Route { Number = 1, Departure = "Иваново", Arrival = "Москва" }
            };
            foreach (Route route in routes)
            {
                baseContext.Route.Add(route);
            }
            baseContext.SaveChanges();

            var flights = new Flight[]
            {
                new Flight { Route = routes[0],  DepartureTime = new DateTime(2020, 4, 01, 18, 00, 00),
                    ArrivalTime = new DateTime(2020, 4, 01, 18, 30, 00), SeatsNumber = 25,
                    BusySeatsNumber = 0 }
            };
            foreach (Flight flight in flights)
            {
                baseContext.Flight.Add(flight);
            }
            baseContext.SaveChanges();
        }
    }
}
