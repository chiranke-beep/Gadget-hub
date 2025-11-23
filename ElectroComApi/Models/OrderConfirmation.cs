using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroComApi.Models
{
    public class OrderConfirmation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int RequestId { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public string Status { get; set; }
        
        // Added columns from Orders table (excluding Price)
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public int QuotationRequestId { get; set; }
        public int QuotationResponseId { get; set; }
        
        public List<ConfirmedProduct> ConfirmedProducts { get; set; } = new List<ConfirmedProduct>();
    }
}