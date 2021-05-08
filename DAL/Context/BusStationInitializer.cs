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
            int cost = 250;
            int seatsNumber = 25;

            if (baseContext.Route.Any())
            {
                return;
            }
            var routes = new Route[]
            {
                new Route { Number = 1, Departure = "Иваново", Arrival = "Москва" },
                new Route { Number = 2, Departure = "Иваново", Arrival = "Ярославль" }
            };
            foreach (Route route in routes)
            {
                baseContext.Route.Add(route);
            }
            baseContext.SaveChanges();

            var flights = new Flight[]
            {
                new Flight { Route = routes[0], DepartureTime = DateTime.Today,
                    ArrivalTime = DateTime.Today, SeatsNumber = seatsNumber,
                    BusySeatsNumber = 0 },
                new Flight { Route = routes[1], DepartureTime = DateTime.Today,
                    ArrivalTime = DateTime.Today, SeatsNumber = seatsNumber,
                    BusySeatsNumber = 0 }
            };
            foreach (Flight flight in flights)
            {
                baseContext.Flight.Add(flight);
            }
            baseContext.SaveChanges();

            var tickets = new Ticket[]
            {
                new Ticket { Flight = flights[0], Cost = cost }
            };
            foreach (Ticket ticket in tickets)
            {
                baseContext.Ticket.Add(ticket);
            }
            baseContext.SaveChanges();
        }
    }
}
