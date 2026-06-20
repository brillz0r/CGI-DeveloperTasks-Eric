using System.ComponentModel.DataAnnotations;

namespace BookingsApi.Models;

public class CreateBookingRequest
{
    [Required]
    public required int RoomId { get; init; }
    [Required]
    public required DateTime From { get; init; }
    [Required]
    public required DateTime To { get; init; }
}