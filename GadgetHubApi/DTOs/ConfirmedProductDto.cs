namespace GadgetHub.DTOs
{
    public class ConfirmedProductDto
    {
        public int Id { get; set; }
        public int OrderConfirmationId { get; set; }
        public string ProductId { get; set; }
        public int QuantityConfirmed { get; set; }
        public string Status { get; set; }
    }
}
