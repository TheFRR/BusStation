using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public  class TicketRepository : IRepository<Ticket>
    {
        private BaseContext db;

        public TicketRepository(BaseContext dbcontext)
        {
            db = dbcontext;
        }

        public async Task<List<Ticket>> GetAll()
        {
            return await db.Ticket.Include(t => t.Flight).ThenInclude(f => f.Route).ToListAsync();
        }

        public async Task<Ticket> Get(int id)
        {
            return await db.Ticket.FindAsync(id);
        }

        public async Task<int> Add(Ticket ticket)
        {
            await db.Ticket.AddAsync(ticket);
            return 1;
        }

        public void Update(Ticket ticket)
        {
            db.Entry(ticket).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var tkt = db.Ticket.Find(id);
            db.Ticket.Remove(tkt);
        }
    }
}
