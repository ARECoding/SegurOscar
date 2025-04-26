using System.ComponentModel.DataAnnotations;
namespace SegurOsCar.Models
{
    public class Client
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Address { get; set; }
        public virtual ICollection<Vehicle> VehicleList { get; set; }

        public Client()
        { }
        public Client(ICollection<Vehicle> listvehicle)
        {
            VehicleList = listvehicle;
        }
    }
}
