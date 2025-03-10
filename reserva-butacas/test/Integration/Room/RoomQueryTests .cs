using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reserva_butacas.Infrastructure.Persistence;
using reserva_butacas.Modules.Billboard.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Room.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository;
using reserva_butacas.test.Integration.Booking;
using Xunit;

namespace reserva_butacas.test.Integration.Room
{
    public class RoomQueryTests(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture = fixture;

        [Fact]
        public async Task GetSeatCountsByRoomForCurrentDay_ShouldReturnCorrectCounts()
        {
            var repository = new SeatRepository(_fixture.DbContext);
            var currentDate = DateTime.Today;

            var result = await repository.GetSeatAvailabilityByRoomForToday();

            Assert.NotNull(result);
            Assert.All(result, roomStats =>
            {
                Assert.True(roomStats.Total >= roomStats.Occupied);
                Assert.Equal(roomStats.Total - roomStats.Occupied, roomStats.Available);
            });
        }
    }
    public class DatabaseFixture : IDisposable
    {
        public AppDbContext DbContext { get; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"RoomTestDatabase_{Guid.NewGuid()}")
                .Options;

            DbContext = new AppDbContext(options);
            SeedDatabase();
        }

        private static void SeedDatabase()
        {
            var today = DateTime.Today;
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }
    }
}