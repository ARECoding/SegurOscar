namespace SegurOsCar.Models
{
    public abstract class Vehicle
    {
        public required string LicencePlate { get; set; }
        public int ModelYear { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public int Kilometers { get; set; }
        
        public required string ClientId { get; set; }
    }
}
