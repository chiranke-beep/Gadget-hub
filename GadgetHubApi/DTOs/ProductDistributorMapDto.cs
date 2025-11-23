namespace GadgetHub.DTOs
{
    public class ProductDistributorMapDto
    {
        public int Id { get; set; }
        public int GadgetHubId { get; set; } // This is the GadgetHub Product ID
        public string? TechWorldId { get; set; }
        public string? GadgetCentralId { get; set; }
        public string? ElectroComId { get; set; }
        
        // Include product information
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
    }
}
