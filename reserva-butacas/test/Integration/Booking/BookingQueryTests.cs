using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Modules.Billboard.Domain.Entities;
using reserva_butacas.Modules.Booking.Domain.Entities;
using reserva_butacas.Modules.Booking.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Customer.Domain.Entities;
using reserva_butacas.Modules.Movie.Domain.Entities;
using reserva_butacas.Modules.Movie.Domain.Enums;
using reserva_butacas.Modules.Room.Domain.Entities;
using reserva_butacas.Modules.Seat.Domain.Entities;
using Xunit;

namespace reserva_butacas.test.Integration.Booking
{
    public class BookingQueryTests(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture = fixture;

        [Fact]
        public async Task GetHorrorMovieBookingsByDateRange_ShouldReturnCorrectBookings()
        {
            var repository = new BookingRepository(_fixture.DbContext);
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 12, 31);

            var result = await repository.GetHorrorMovieBookingsInDateRange(startDate, endDate);

            Assert.NotNull(result);
            Assert.All(result, booking =>
            {
                Assert.Equal(MovieGenreEnum.HORROR, booking.Billboard.Movie.Genre);
                Assert.True(booking.Date >= startDate && booking.Date <= endDate);
            });
        }
    }

    public class DatabaseFixture : IDisposable
    {
        public AppDbContext DbContext { get; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"TestDatabase_{Guid.NewGuid()}")
                .Options;

            DbContext = new AppDbContext(options);
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Add test data
            var room = new RoomEntity { Name = "Test Room", Number = 1 };
            DbContext.Rooms.Add(room);

            var movie = new MovieEntity
            {
                Name = "Horror Movie",
                Genre = MovieGenreEnum.HORROR,
                AllowedAge = 18,
                LengthMinutes = 120
            };
            DbContext.Movies.Add(movie);

            var billboard = new BillboardEntity
            {
                Id = 1,
                Date = new DateTime(2023, 6, 15),
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(20, 0, 0),
                MovieID = 1,
                RoomID = 1,
                Movie = movie,
                Room = room

            };
            DbContext.Billboards.Add(billboard);

            var seat = new SeatEntity
            {
                Number = 1,
                RowNumber = 1,
                RoomID = 1,
                Room = room
            };
            DbContext.Seats.Add(seat);

            var customer = new CustomerEntity
            {
                DocumentNumber = "123456",
                Name = "John",
                Lastname = "Doe",
                Age = 25,
                Email = "john@example.com",
                PhoneNumber = "123456789"
            };
            DbContext.Customers.Add(customer);

            var booking = new BookingEntity
            {
                Date = new DateTime(2023, 6, 15),
                CustomerID = 1,
                SeatID = 1,
                BillboardID = 1,
                Seat = seat,
                Customer = customer,
                Billboard = billboard
            };
            DbContext.Bookings.Add(booking);

            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }

    }
}