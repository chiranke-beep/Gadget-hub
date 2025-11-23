using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GadgetHub.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; } // Make optional for model binding
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        
        // Added columns from FinalResponse table
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int QuotationRequestId { get; set; }
        public int QuotationResponseId { get; set; }
        
        // Navigation property to Product
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
