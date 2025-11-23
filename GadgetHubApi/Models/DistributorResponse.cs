using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GadgetHub.Models
{
    public class DistributorResponse
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        [MaxLength(100)]
        public string DistributorName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ResponseData { get; set; }

        public DateTime ResponseDate { get; set; }

        public string Status { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
} 