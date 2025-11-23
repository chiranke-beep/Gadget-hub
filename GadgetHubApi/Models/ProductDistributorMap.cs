using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GadgetHub.Models
{
    public class ProductDistributorMap
    {
        [Key]
        public int Id { get; set; }
        
        // GadgetHub product ID (primary product ID)
        [Required]
        public int GadgetHubId { get; set; }
        
        // Cross-distributor ID mapping (strings because distributor APIs expect string ProductIds)
        public string? TechWorldId { get; set; }
        public string? GadgetCentralId { get; set; }
        public string? ElectroComId { get; set; }
        
        // Navigation property to Product
        [ForeignKey("GadgetHubId")]
        public virtual Product Product { get; set; }
    }
}
