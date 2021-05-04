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
    public class UserRepository : IRepository<User>
    {
        private BaseContext db;

        public UserRepository(BaseContext dbcontext)
        {
            db = dbcontext;
        }

        public async Task<List<User>> GetAll()
        {
            return await db.User.ToListAsync();
        }

        public async Task<User> Get(int id)
        {
            return await db.User.FindAsync(id);
        }

        public async Task<int> Add(User user)
        {
            await db.User.AddAsync(user);
            return 1;
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var usr = db.User.Find(id);
            db.User.Remove(usr);
        }
    }
}
