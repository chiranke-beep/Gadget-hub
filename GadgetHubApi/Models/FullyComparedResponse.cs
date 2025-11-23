using System.ComponentModel.DataAnnotations;

namespace GadgetHub.Models
{
    public class FullyComparedResponse
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int QuotationRequestId { get; set; }
        public int QuotationResponseId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
