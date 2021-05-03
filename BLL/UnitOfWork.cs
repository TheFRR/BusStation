using DAL.Context;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private BaseContext db;

        public UnitOfWork(BaseContext db)
        {
            this.db = db;
        }

        private FlightRepository flightRepository;
        private RouteRepository routeRepository;
        private TicketRepository ticketRepository;
        private UserRepository userRepository;
        private BoughtTicketsRepository boughtTicketsRepository;

        public FlightRepository Flight
        {
            get
            {
                if (flightRepository == null)
                    flightRepository = new FlightRepository(db);
                return flightRepository;
            }
        }

        public RouteRepository Route
        {
            get
            {
                if (routeRepository == null)
                    routeRepository = new RouteRepository(db);
                return routeRepository;
            }
        }

        public TicketRepository Ticket
        {
            get
            {
                if (ticketRepository == null)
                    ticketRepository = new TicketRepository(db);
                return ticketRepository;
            }
        }

        public UserRepository User
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public BoughtTicketsRepository BoughtTickets
        {
            get
            {
                if (boughtTicketsRepository == null)
                    boughtTicketsRepository = new BoughtTicketsRepository(db);
                return boughtTicketsRepository;
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
