using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using reserva_butacas.Domain.Ports;
using reserva_butacas.Modules.Billboard.Domain.Entities;
using reserva_butacas.Modules.Billboard.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Booking.Aplication.DTOs;
using reserva_butacas.Modules.Booking.Aplication.Services;
using reserva_butacas.Modules.Booking.Domain.Entities;
using reserva_butacas.Modules.Booking.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Customer.Domain.Entities;
using reserva_butacas.Modules.Customer.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Movie.Domain.Entities;
using reserva_butacas.Modules.Movie.Domain.Enums;
using reserva_butacas.Modules.Room.Domain.Entities;
using reserva_butacas.Modules.Room.Infrastructure.Persistence.Repository;
using reserva_butacas.Modules.Seat.Aplication.DTOs;
using reserva_butacas.Modules.Seat.Aplication.Services;
using reserva_butacas.Modules.Seat.Domain.Entities;
using reserva_butacas.Modules.Seat.Infrastructure.Persistence.Repository;
using Xunit;

namespace reserva_butacas.test.Unit.Booking
{
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        private readonly Mock<ISeatRepository> _seatRepositoryMock;
        private readonly BookingService _bookingService;
        private readonly SeatService _seatService;

        public BookingServiceTests()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _seatRepositoryMock = new Mock<ISeatRepository>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var billboardRepositoryMock = new Mock<IBillboardRepository>();
            var mapperMock = new Mock<IMapper>();

            _bookingService = new BookingService(
                _bookingRepositoryMock.Object,
                customerRepositoryMock.Object,
                _seatRepositoryMock.Object,
                billboardRepositoryMock.Object,
                mapperMock.Object
            );

            var roomRepositoryMock = new Mock<IRoomRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            _seatService = new SeatService(
                _seatRepositoryMock.Object,
                _bookingRepositoryMock.Object,
                unitOfWorkMock.Object,
                mapperMock.Object,
                roomRepositoryMock.Object
            );
        }

        [Fact]
        public async Task DisableSeatAndCancelReservation_ShouldSucceed_WhenValidInput()
        {
            var bookingId = 1;
            var seatId = 2;
            var billboardId = 3;

            var CustomerEntity = new CustomerEntity
            {
                Id = 1,
                Name = "John",
                DocumentNumber = "12345678",
                Lastname = "Doe",
                Email = "sfsa@gmail.com",
                PhoneNumber = "123456789",
                Status = true
            };
            var RoomEntity = new RoomEntity
            {
                Id = 1,
                Name = "Room 1",
                Status = true,
                Seats = []
            };

            var movieEntity = new MovieEntity
            {
                Id = 1,
                Name = "Movie 1",
                AllowedAge = 18,
                Genre = MovieGenreEnum.HORROR,
                Status = true
            };

            var seat = new SeatEntity
            {
                Id = seatId,
                Number = 1,
                RowNumber = 1,
                RoomID = RoomEntity.Id,
                Room = RoomEntity,
                Status = true
            };
            var billboardEntity = new BillboardEntity
            {
                Id = billboardId,
                Movie = movieEntity,
                Room = RoomEntity,
                Status = true
            };

            var bookingEntity = new BookingEntity
            {
                Id = bookingId,
                CustomerID = CustomerEntity.Id,
                Customer = CustomerEntity,
                SeatID = seat.Id,
                Seat = seat,
                BillboardID = billboardId,
                Billboard = billboardEntity,
                Status = true
            };



            _bookingRepositoryMock.Setup(repo => repo.GetByIdAsync(bookingId))
                .ReturnsAsync(bookingEntity);

            _seatRepositoryMock.Setup(repo => repo.GetByIdAsync(seatId))
                .ReturnsAsync(seat);

            _bookingRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<BookingEntity>()))
                .Returns(Task.CompletedTask);

            _seatRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<SeatEntity>()))
                .Returns(Task.CompletedTask);


            await _seatService.CancelSeatAndBookingAsync(1);

            _bookingRepositoryMock.Verify(repo => repo.GetByIdAsync(bookingId), Times.Once);
            _seatRepositoryMock.Verify(repo => repo.GetByIdAsync(seatId), Times.Once);

            _bookingRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<BookingEntity>(b =>
                b.Id == bookingId && b.Status == false)), Times.Once);

            _seatRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<SeatEntity>(s =>
                s.Id == seatId && s.Status == false)), Times.Once);
        }

    }
}