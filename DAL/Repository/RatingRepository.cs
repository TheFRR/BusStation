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
    public class RatingRepository : IRepository<Rating>
    {

        private BaseContext db;

        public RatingRepository(BaseContext dbcontext)
        {
            db = dbcontext;
        }

        public async Task<List<Rating>> GetAll()
        {
            return await db.Rating.Include(u => u.User).Include(r => r.Route).
                ToListAsync();
        }

        public async Task<Rating> Get(int id)
        {
            return await db.Rating.FindAsync(id);
        }

        public async Task<int> Add(Rating rating)
        {
            await db.Rating.AddAsync(rating);
            return 1;
        }

        public void Update(Rating rating)
        {
            db.Entry(rating).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var ticket = db.Rating.Find(id);
            db.Rating.Remove(ticket);
        }
    }
}
