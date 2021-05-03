using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IUnitOfWork
    {
        FlightRepository Flight { get; }
        RouteRepository Route { get; }
        TicketRepository Ticket { get; }
        BoughtTicketsRepository BoughtTickets { get; }
        UserRepository User { get; }

        int Save();
    }
}
