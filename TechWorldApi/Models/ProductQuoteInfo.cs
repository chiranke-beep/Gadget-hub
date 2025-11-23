using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechWorld.Models
{
    public class ProductQuoteInfo
    {
        [Key]
        public int Id { get; set; }
        public int QuotationResponseId { get; set; }
        public string ProductId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int AvailableQuantity { get; set; } // Non-nullable int
        public int EstimatedDeliveryDays { get; set; }

        [ForeignKey("QuotationResponseId")]
        public QuotationResponse QuotationResponse { get; set; }
    }
}