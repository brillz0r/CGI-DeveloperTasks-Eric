namespace BookingsApi.Models
{
    public class Booking
    {
        public int Id { get; init; }
        public int RoomId { get; init; }
        public DateTime From { get; init; }
        public DateTime To { get; init; }
    }
}
