using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Domain.Entities;
using reserva_butacas.Modules.Billboard.Domain.Entities;
using reserva_butacas.Modules.Booking.Domain.Entities;
using reserva_butacas.Modules.Customer.Domain.Entities;
using reserva_butacas.Modules.Movie.Domain.Entities;
using reserva_butacas.Modules.Room.Domain.Entities;
using reserva_butacas.Modules.Seat.Domain.Entities;

namespace reserva_butacas.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<BillboardEntity> Billboards { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<SeatEntity> Seats { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerEntity>()
            .HasIndex(c => c.DocumentNumber)
            .IsUnique();

            modelBuilder.Entity<BookingEntity>()
                    .HasOne(b => b.Customer)
                    .WithMany()
                    .HasForeignKey(b => b.CustomerID)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookingEntity>()
                   .HasOne(b => b.Seat)
                   .WithMany()
                   .HasForeignKey(b => b.SeatID)
                   .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookingEntity>()
                    .HasOne(b => b.Billboard)
                    .WithMany()
                    .HasForeignKey(b => b.BillboardID)
                    .OnDelete(DeleteBehavior.NoAction);
        }


    }
}