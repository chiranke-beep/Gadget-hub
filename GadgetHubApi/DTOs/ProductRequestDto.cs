namespace GadgetHub.DTOs
{
    public class ProductRequestDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public string RequestedBy { get; set; }
    }
}
