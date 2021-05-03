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
    public class RouteRepository : IRepository<Route>
    {
        private BaseContext db;

        public RouteRepository(BaseContext dbcontext)
        {
            db = dbcontext;
        }

        public async Task<List<Route>> GetAll()
        {
            return await db.Route.ToListAsync();
        }

        public async Task<Route> Get(int id)
        {
            return await db.Route.FindAsync(id);
        }

        public async Task<int> Add(Route route)
        {
            await db.Route.AddAsync(route);
            return 1;
        }

        public async Task<int> Update(Route route)
        {
            var rt = await db.Route.FindAsync(route.Id);
            db.Entry(rt).CurrentValues.SetValues(route);
            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var rt = await db.Route.FindAsync(id);
            db.Route.Remove(rt);
            return 1;
        }

    }
}
