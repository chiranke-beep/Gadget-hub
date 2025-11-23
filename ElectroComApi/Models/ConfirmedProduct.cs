using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroComApi.Models
{
    public class ConfirmedProduct
    {
        [Key]
        public int Id { get; set; }
        public int OrderConfirmationId { get; set; }
        public string ProductId { get; set; }
        public int QuantityConfirmed { get; set; }
        public string Status { get; set; } = "Confirmed"; // Default status

        [ForeignKey("OrderConfirmationId")]
        public OrderConfirmation OrderConfirmation { get; set; }
    }
}