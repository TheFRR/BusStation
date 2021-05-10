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
            DateTime todayDate = DateTime.Today;

            if (baseContext.Route.Any())
            {
                return;
            }
            var routes = new Route[]
            {
                new Route { Number = 1, Departure = "Иваново", Arrival = "Москва" },
                new Route { Number = 2, Departure = "Иваново", Arrival = "Ярославль" },
                new Route { Number = 3, Departure = "Иваново", Arrival = "Владимир" }
            };
            foreach (Route route in routes)
            {
                baseContext.Route.Add(route);
            }
            baseContext.SaveChanges();

            List<Flight> flights = new List<Flight>();

            for (int i = todayDate.Day; i <= DateTime.DaysInMonth(todayDate.Year, todayDate.Month); i++)
            {
                foreach (Route route in routes)
                {
                    flights.Add(new Flight()
                    {
                        Route = route,
                        DepartureTime = new DateTime(Convert.ToInt32(todayDate.Year), Convert.ToInt32(todayDate.Month), Convert.ToInt32(i), 8, 0, 0),
                        ArrivalTime = new DateTime(Convert.ToInt32(todayDate.Year), Convert.ToInt32(todayDate.Month), Convert.ToInt32(i), 10, 0, 0),
                        SeatsNumber = seatsNumber,
                        BusySeatsNumber = 0
                    });
                    flights.Add(new Flight()
                    {
                        Route = route,
                        DepartureTime = new DateTime(Convert.ToInt32(todayDate.Year), Convert.ToInt32(todayDate.Month), Convert.ToInt32(i), 18, 0, 0),
                        ArrivalTime = new DateTime(Convert.ToInt32(todayDate.Year), Convert.ToInt32(todayDate.Month), Convert.ToInt32(i), 19, 45, 0),
                        SeatsNumber = seatsNumber,
                        BusySeatsNumber = 0
                    });
                }
            }

            foreach (Flight flight in flights)
            {
                baseContext.Flight.Add(flight);
            }
            baseContext.SaveChanges();

            List<Ticket> tickets = new List<Ticket>();

            foreach (Flight flight in flights)
            {
                Ticket ticket = new Ticket() { Flight = flight, Cost = cost };
                tickets.Add(ticket);
            }

            foreach (Ticket ticket in tickets)
            {
                baseContext.Ticket.Add(ticket);
            }
            baseContext.SaveChanges();

        }
    }
}
