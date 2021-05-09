using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Context
{
    public class BaseContext : IdentityDbContext<User>
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<BoughtTicket> BoughtTicket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
