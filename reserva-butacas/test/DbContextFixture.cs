using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Modules.Billboard.Domain.Entities;
using reserva_butacas.Modules.Booking.Domain.Entities;
using reserva_butacas.Modules.Customer.Domain.Entities;
using reserva_butacas.Modules.Movie.Domain.Entities;
using reserva_butacas.Modules.Movie.Domain.Enums;
using reserva_butacas.Modules.Room.Domain.Entities;
using reserva_butacas.Modules.Seat.Domain.Entities;

namespace reserva_butacas.test
{
    public class DbContextFixture : IDisposable
    {
        public AppDbContext Context { get; private set; }

        public DbContextFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TestDatabase_{Guid.NewGuid()}")
                .Options;

            Context = new AppDbContext(options);

            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            SeedTestData();
        }

        private void SeedTestData()
        {
            var room1 = new RoomEntity { Name = "Sala 1", Number = 1, Status = true, Seats = [] };
            var room2 = new RoomEntity { Name = "Sala 2", Number = 2, Status = true, Seats = [] };
            Context.Rooms.AddRange(room1, room2);
            Context.SaveChanges();

            var horrorMovie = new MovieEntity
            {
                Name = "El Exorcista",
                Genre = MovieGenreEnum.HORROR,
                AllowedAge = 18,
                LengthMinutes = 120,
                Status = true
            };
            var comedyMovie = new MovieEntity
            {
                Name = "La Máscara",
                Genre = MovieGenreEnum.COMEDY,
                AllowedAge = 12,
                LengthMinutes = 95,
                Status = true
            };
            Context.Movies.AddRange(horrorMovie, comedyMovie);
            Context.SaveChanges();

            var seats = new List<SeatEntity>();
            for (short row = 1; row <= 5; row++)
            {
                for (short num = 1; num <= 10; num++)
                {
                    seats.Add(new SeatEntity
                    {
                        RowNumber = row,
                        Number = num,
                        RoomID = room1.Id,
                        Room = room1,
                        Status = true
                    });

                    seats.Add(new SeatEntity
                    {
                        RowNumber = row,
                        Number = num,
                        RoomID = room2.Id,
                        Room = room2,
                        Status = true
                    });
                }
            }
            Context.Seats.AddRange(seats);
            Context.SaveChanges();

            var billboard1 = new BillboardEntity
            {
                Date = DateTime.Today.AddDays(1),
                StartTime = new TimeSpan(19, 0, 0),
                EndTime = new TimeSpan(21, 0, 0),
                MovieID = horrorMovie.Id,
                Movie = horrorMovie,
                RoomID = room1.Id,
                Room = room1,
                Status = true
            };

            var billboard2 = new BillboardEntity
            {
                Date = DateTime.Today.AddDays(-1),
                StartTime = new TimeSpan(19, 0, 0),
                EndTime = new TimeSpan(21, 0, 0),
                MovieID = comedyMovie.Id,
                Movie = comedyMovie,
                RoomID = room2.Id,
                Room = room2,
                Status = true
            };

            Context.Billboards.AddRange(billboard1, billboard2);
            Context.SaveChanges();

            var customer1 = new CustomerEntity
            {
                DocumentNumber = "12345678",
                Name = "Juan",
                Lastname = "Pérez",
                Age = 25,
                PhoneNumber = "123-456-7890",
                Email = "juan.perez@example.com",
                Status = true
            };

            var customer2 = new CustomerEntity
            {
                DocumentNumber = "87654321",
                Name = "María",
                Lastname = "López",
                Age = 30,
                PhoneNumber = "098-765-4321",
                Email = "maria.lopez@example.com",
                Status = true
            };

            Context.Customers.AddRange(customer1, customer2);
            Context.SaveChanges();

            var booking1 = new BookingEntity
            {
                Date = DateTime.Today,
                CustomerID = customer1.Id,
                Customer = customer1,
                SeatID = seats[0].Id,
                Seat = seats[0],
                BillboardID = billboard1.Id,
                Billboard = billboard1,
                Status = true
            };

            var booking2 = new BookingEntity
            {
                Date = DateTime.Today.AddDays(-1),
                CustomerID = customer2.Id,
                Customer = customer2,
                SeatID = seats[50].Id,
                Seat = seats[50],
                BillboardID = billboard2.Id,
                Billboard = billboard2,
                Status = true
            };

            Context.Bookings.AddRange(booking1, booking2);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}