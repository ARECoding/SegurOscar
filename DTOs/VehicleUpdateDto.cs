namespace SegurOsCar.DTOs
{
    public class VehicleUpdateDto
    {

        public required string LicencePlate { get; set; }
        public int ModelYear { get; set; }
        public string? Brand { get; set; }

        public string? Model { get; set; }

        public int Kilometers { get; set; }
    }
}
