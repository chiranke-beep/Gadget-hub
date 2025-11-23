using System;
using System.Collections.Generic;

namespace Gadget_CentralApi.DTO
{
    public class OrderConfirmationDto
    {
        public int OrderId { get; set; } // Changed from string to int
        public int RequestId { get; set; } // Changed from string to int
        public DateTime ConfirmationDate { get; set; }
        public string Status { get; set; }
        public List<ConfirmedProductDto> ConfirmedProducts { get; set; } = new();
    }
}