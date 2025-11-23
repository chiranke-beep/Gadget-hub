using System.ComponentModel.DataAnnotations;

namespace TechWorld.DTO
{
    public class ProductRequestDto
    {
        [Required]
        public string ProductId { get; set; }
        
        public int Quantity { get; set; }
        
        public int UserId { get; set; }
        
        [Required]
        public string RequestedBy { get; set; }
    }
}
