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
    public class BoughtTicketsRepository : IRepository<BoughtTickets>
    {
        private BaseContext db;

        public BoughtTicketsRepository(BaseContext dbcontext)
        {
            db = dbcontext;
        }

        public async Task<List<BoughtTickets>> GetAll()
        {
            return await db.BoughtTickets.ToListAsync();
        }

        public async Task<BoughtTickets> Get(int id)
        {
            return await db.BoughtTickets.FindAsync(id);
        }

        public async Task<int> Add(BoughtTickets ticket)
        {
            await db.BoughtTickets.AddAsync(ticket);
            return 1;
        }

        public void Update(BoughtTickets ticket)
        {
            db.Entry(ticket).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var ticket = db.BoughtTickets.Find(id);
            db.BoughtTickets.Remove(ticket);
        }
    }
}
