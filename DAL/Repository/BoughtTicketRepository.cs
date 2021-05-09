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
    public class BoughtTicketRepository : IRepository<BoughtTicket>
    {
        private BaseContext db;

        public BoughtTicketRepository(BaseContext dbcontext)
        {
            db = dbcontext;
        }

        public async Task<List<BoughtTicket>> GetAll()
        {
            return await db.BoughtTicket.Include(u => u.User).Include(t => t.Ticket).
                ThenInclude(f => f.Flight).ThenInclude(r => r.Route).
                ToListAsync();
        }

        public async Task<BoughtTicket> Get(int id)
        {
            return await db.BoughtTicket.FindAsync(id);
        }

        public async Task<int> Add(BoughtTicket ticket)
        {
            await db.BoughtTicket.AddAsync(ticket);
            return 1;
        }

        public void Update(BoughtTicket ticket)
        {
            db.Entry(ticket).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var ticket = db.BoughtTicket.Find(id);
            db.BoughtTicket.Remove(ticket);
        }
    }
}
