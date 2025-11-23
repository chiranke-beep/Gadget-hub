using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroComApi.Models
{
    public class ProductRequestInfo
    {
        [Key]
        public int Id { get; set; }
        public int QuotationRequestId { get; set; }
        
        [Required]
        public string ProductId { get; set; }
        
        public int Quantity { get; set; }
        public int UserId { get; set; }
        
        [Required]
        public string RequestedBy { get; set; }

        [ForeignKey("QuotationRequestId")]
        public QuotationRequest QuotationRequest { get; set; }
    }
}