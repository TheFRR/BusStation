using DAL.Context;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    /// <summary>
    /// Класс, предоставляющий интерфейс для взаимодействия с репозиториями
    /// </summary>
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
        private BoughtTicketRepository boughtTicketRepository;
        private RatingRepository ratingRepository;

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

        public BoughtTicketRepository BoughtTicket
        {
            get
            {
                if (boughtTicketRepository == null)
                    boughtTicketRepository = new BoughtTicketRepository(db);
                return boughtTicketRepository;
            }
        }

        public RatingRepository Rating
        {
            get
            {
                if (ratingRepository == null)
                    ratingRepository = new RatingRepository(db);
                return ratingRepository;
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
