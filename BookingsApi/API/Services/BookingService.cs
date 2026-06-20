using BookingsApi.Models;
using BookingsApi.Repositories;

namespace BookingsApi.Services
{
    public interface IBookingService
    {
        Result<IEnumerable<Booking>> GetAll();
        Result<Booking> Create(CreateBookingRequest booking);
        Result Cancel(int id);
        Result<Booking> GetById(int id);
    }

    public class BookingService(IBookingRepository _repo) : IBookingService
    {
        private static readonly object _bookingLock = new();

        public Result<IEnumerable<Booking>> GetAll()
        {
            var bookings = _repo.GetAll();
            return Result<IEnumerable<Booking>>.Success(bookings);
        }
        
        public Result<Booking> Create(CreateBookingRequest booking)
        {
            lock (_bookingLock)
            {
                if (HasOverlap(booking.RoomId, booking.From, booking.To))
                {
                    return Result<Booking>.Conflict("Booking overlaps");
                }

                var newBooking = _repo.Add(booking);
                return Result<Booking>.Success(newBooking);
            }
        }

        public Result Cancel(int id)
        {
            var booking = _repo.GetById(id);
            if (booking == null)
            {
                return Result.NotFound("Booking not found");
            }
            
            _repo.Delete(id);
            return Result.Success();
        }

        public Result<Booking> GetById(int id)
        {
            var booking = _repo.GetById(id);
            if (booking == null)
            {
                return Result<Booking>.NotFound("Booking not found");
            }
            
            return Result<Booking>.Success(booking);
        }
        
        /// <summary>
        /// Returnerar true om tidsintervallet krockar med befintlig bokning
        /// </summary>
        private bool HasOverlap(int roomId, DateTime from, DateTime to)
        {
            return _repo
                .GetAll()
                .Any(b =>
                    b.RoomId == roomId &&
                    b.To > from && b.From < to
                );
        }
    }
}
