using System.ComponentModel.DataAnnotations;
namespace SegurOsCar.Models
{
    public class Client
    {
        public required string ClientId { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Address { get; set; }

        public Client()
        { }
    }
}
