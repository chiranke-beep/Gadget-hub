using System.ComponentModel.DataAnnotations;
        
namespace GadgetHub.DTOs
{
    public class ProductDistributorMapWriteDto
    {
        [Required]
        public int GadgetHubId { get; set; } // This is the GadgetHub Product ID
        
        public string? TechWorldId { get; set; }
        public string? GadgetCentralId { get; set; }
        public string? ElectroComId { get; set; }
    }
}
