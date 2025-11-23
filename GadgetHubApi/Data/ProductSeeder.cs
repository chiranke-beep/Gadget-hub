using GadgetHub.Models;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.Data
{
    public static class ProductSeeder
    {
        public static void SeedProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Wireless Mouse", Description = "Ergonomic wireless mouse with adjustable DPI.", Brand = "Logitech", Category = "Peripherals", Url = "https://images.unsplash.com/photo-1517336714731-489689fd1ca8" },
                new Product { ProductId = 2, Name = "Mechanical Keyboard", Description = "RGB backlit mechanical keyboard with blue switches.", Brand = "Keychron", Category = "Peripherals", Url = "https://images.unsplash.com/photo-1519389950473-47ba0277781c" },
                new Product { ProductId = 3, Name = "Noise Cancelling Headphones", Description = "Over-ear headphones with active noise cancellation.", Brand = "Sony", Category = "Accessories", Url = "https://images.unsplash.com/photo-1511367461989-f85a21fda167" },
                new Product { ProductId = 4, Name = "4K Monitor", Description = "27-inch 4K UHD monitor with HDR support.", Brand = "Dell", Category = "Monitors", Url = "https://images.unsplash.com/photo-1519125323398-675f0ddb6308" },
                new Product { ProductId = 5, Name = "USB-C Hub", Description = "Multiport USB-C hub with HDMI, USB 3.0, and SD card reader.", Brand = "Anker", Category = "Accessories", Url = "https://images.unsplash.com/photo-1465101046530-73398c7f28ca" },
                new Product { ProductId = 6, Name = "Portable SSD", Description = "1TB portable SSD with USB 3.2 Gen 2 support.", Brand = "Samsung", Category = "Storage", Url = "https://images.unsplash.com/photo-1518770660439-4636190af475" },
                new Product { ProductId = 7, Name = "Smartwatch", Description = "Fitness tracking smartwatch with heart rate monitor.", Brand = "Apple", Category = "Wearables", Url = "https://images.unsplash.com/photo-1516574187841-cb9cc2ca948b" },
                new Product { ProductId = 8, Name = "Bluetooth Speaker", Description = "Waterproof Bluetooth speaker with 12-hour battery life.", Brand = "JBL", Category = "Audio", Url = "https://images.unsplash.com/photo-1509395176047-4a66953fd231" },
                new Product { ProductId = 9, Name = "Webcam", Description = "1080p HD webcam with built-in microphone.", Brand = "Logitech", Category = "Peripherals", Url = "https://images.unsplash.com/photo-1515378791036-0648a3ef77b2" },
                new Product { ProductId = 10, Name = "Wireless Charger", Description = "Fast wireless charging pad for smartphones.", Brand = "Belkin", Category = "Accessories", Url = "https://images.unsplash.com/photo-1512446733611-9099a758e082" }
            );
        }
    }
}
