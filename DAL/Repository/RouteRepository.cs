using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Route rt = await db.Route.FindAsync(route.Id);
            db.Entry(rt).CurrentValues.SetValues(route);
            return 1;
        }

        public void Delete(int id)
        {
            Route rt = db.Route.FirstOrDefault(r => r.Id == id);
            if (rt != null)
            {
                db.Route.Remove(rt);
            }
        }

    }
}
