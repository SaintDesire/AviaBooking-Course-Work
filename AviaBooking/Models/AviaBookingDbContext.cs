using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace AviaBooking.Models
{
    public class AviaBookingDbContext : DbContext
    {
        public AviaBookingDbContext()
        {
        }

        public virtual DbSet<Airline> Airlines { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-T94EMTB;Database=AviaBooking;Trusted_Connection=True;TrustServerCertificate=true");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>().HasKey(a => a.Id);
            modelBuilder.Entity<Client>().HasKey(c => c.Id);
            modelBuilder.Entity<Flight>().HasKey(f => f.Id);
            modelBuilder.Entity<Payment>().HasKey(p => p.Id);
            modelBuilder.Entity<Passenger>().HasKey(p => p.Id);
            modelBuilder.Entity<Review>().HasKey(r => r.Id);
        }

    }

}
