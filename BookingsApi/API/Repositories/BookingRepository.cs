using BookingsApi.Models;

namespace BookingsApi.Repositories
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll();
        Booking? GetById(int id);
        Booking Add(CreateBookingRequest booking);
        void Delete(int id);
    }

    public class BookingRepository : IBookingRepository
    {
        private static readonly List<Booking> _bookings = [];
        private static int _nextId = 1;

        public IEnumerable<Booking> GetAll()
        {
            return _bookings;
        }

        public Booking? GetById(int id)
        {
            return _bookings.FirstOrDefault(b => b.Id == id);
        }

        public Booking Add(CreateBookingRequest booking)
        {
            var newBooking = new Booking
            {
                Id = _nextId++,
                RoomId = booking.RoomId,
                From = booking.From,
                To = booking.To,
            };
            
            _bookings.Add(newBooking);
            return newBooking;
        }

        public void Delete(int id)
        {
            var booking = GetById(id);
            if (booking != null)
            {
                _bookings.Remove(booking);
            }
        }
    }
}
