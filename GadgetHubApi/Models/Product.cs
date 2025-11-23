using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GadgetHub.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public string? Url { get; set; } // Product photo URL
        
        // Navigation property to ProductDistributorMap
        public virtual ProductDistributorMap ProductDistributorMap { get; set; }
    }
}
