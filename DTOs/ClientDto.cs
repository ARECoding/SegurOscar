using SegurOsCar.Models;

namespace SegurOsCar.DTOs
{
    public class ClientDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string Email { get; set; }

        public string? Address { get; set; }
        public required ICollection<VehicleDto> Vehicles { get; set; }

    }
}
