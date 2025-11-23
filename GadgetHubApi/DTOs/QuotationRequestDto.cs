namespace GadgetHub.DTOs
{
    public class QuotationRequestDto
    {
        public int RequestId { get; set; } // Changed to int
        public List<string> ProductIds { get; set; }
        public int Quantity { get; set; }
        // Add other fields as needed for distributor APIs
    }
}
