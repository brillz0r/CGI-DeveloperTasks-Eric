using Xunit;
using BookingsApi.Models;
using BookingsApi.Services;
using BookingsApi.Repositories;
using System;

namespace BookingsApi.Tests
{
    public class InMemoryBookingRepository : BookingRepository { }

    public class BookingServiceTests
    {
        [Fact]
        public void CreateBooking_RejectsOverlap()
        {
            var repo = new InMemoryBookingRepository();
            var svc = new BookingService(repo);

            var firstBookingResult = svc.Create(new CreateBookingRequest
            {
                RoomId = 1,
                From = new DateTime(2025, 1, 1, 9, 0, 0),
                To = new DateTime(2025, 1, 1, 12, 0, 0)
            });

            var secondBookingResult = svc.Create(new CreateBookingRequest
            {
                RoomId = 1,
                From = new DateTime(2025, 1, 1, 11, 0, 0),
                To = new DateTime(2025, 1, 1, 13, 0, 0)
            });
            
            Assert.Equal(1, firstBookingResult.Value.RoomId);
            Assert.False(secondBookingResult.IsSuccess);
            Assert.Equal(ResultType.Conflict, secondBookingResult.Type);
        }

        [Fact]
        public void Cancel_RemovesBooking()
        {
            var repo = new InMemoryBookingRepository();
            var svc = new BookingService(repo);

            var booking = svc.Create(new CreateBookingRequest
            {
                RoomId = 2,
                From = new DateTime(2025, 1, 2, 10, 0, 0),
                To = new DateTime(2025, 1, 2, 11, 0, 0)
            });

            svc.Cancel(booking.Value!.Id);

            var cancelledBooking = svc.GetById(booking.Value.Id);

            Assert.False(cancelledBooking.IsSuccess);
            Assert.Equal(ResultType.NotFound, cancelledBooking.Type);
        }

        [Fact]
        public void CreateBooking_RejectsOverlap2()
        {
            var repo = new InMemoryBookingRepository();
            var svc = new BookingService(repo);

            var b1 = svc.Create(new CreateBookingRequest
            {
                RoomId = 3,
                From = new DateTime(2025, 1, 5, 0, 0, 0),
                To = new DateTime(2025, 1, 6, 0, 0, 0)
            });

            var b2 = svc.Create(new CreateBookingRequest
            {
                RoomId = 3,
                From = new DateTime(2025, 1, 4, 0, 0, 0),
                To = new DateTime(2025, 1, 5, 0, 0, 0)
            });

            Assert.Equal(b1.Value.RoomId, b2.Value.RoomId);
        }
    }
}
