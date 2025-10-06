namespace AbadiAgroApi.Model
{
    public class SeedPacketVerificationValidModel
    {
        public string Status { get; set; }
        public string Manufacturer { get; set; }
        public string Crop { get; set; }
        public string Batch { get; set; }
        public string ExpiryDate { get; set; }
        public string Registered { get; set; }
        public string Authentic { get; set; }

    }
    public class SeedPacketVerificationFake
    {
        public string Status { get; set; }
        public string Authentic { get; set; }
        public string Message { get; set; }
    }
}
