using System;
using System.Collections.Generic;

namespace GadgetHub.DTOs
{
    public class OrderConfirmationDto
    {
        public int OrderId { get; set; }
        public string RequestId { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public string Status { get; set; }
        public List<ConfirmedProductDto> ConfirmedProducts { get; set; } = new();
    }
}
