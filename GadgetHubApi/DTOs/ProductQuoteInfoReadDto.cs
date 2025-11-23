namespace GadgetHub.DTOs
{
    public class ProductQuoteInfoReadDto
    {
        public string ProductId { get; set; }
        public decimal PricePerUnit { get; set; }
        public int AvailableQuantity { get; set; }
        public int EstimatedDeliveryDays { get; set; }
    }
}
