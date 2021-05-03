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
    public class FlightRepository : IRepository<Flight>
    {
        private BaseContext db;

        public FlightRepository(BaseContext dbcontext)
        {
            db = dbcontext;
        }

        public async Task<List<Flight>> GetAll()
        {
            return await db.Flight.ToListAsync();
        }

        public async Task<Flight> Get(int id)
        {
            return await db.Flight.FindAsync(id);
        }

        public async Task<int> Add(Flight flight)
        {
            await db.Flight.AddAsync(flight);
            return 1;
        }

        public async Task<int> Update(Flight flight)
        {
            var flt = await db.Flight.FindAsync(flight);
            db.Entry(flt).CurrentValues.SetValues(flight);
            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var flt = await db.Flight.FindAsync(id);
            db.Flight.Remove(flt);
            return 1;
        }
    }
}
