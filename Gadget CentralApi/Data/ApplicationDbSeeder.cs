using Microsoft.EntityFrameworkCore;
using Gadget_CentralApi.Models;
using System;
using System.Linq;

namespace Gadget_CentralApi.Data
{
    public static class ApplicationDbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.QuotationRequests.Any())
            {
                var quotationRequest = new QuotationRequest
                {
                    RequestDate = new DateTime(2025, 7, 23, 17, 50, 0, DateTimeKind.Utc) // July 23, 2025, 05:50 PM UTC
                };
                context.QuotationRequests.Add(quotationRequest);
                context.SaveChanges();

                context.ProductRequestInfos.AddRange(new List<ProductRequestInfo>
                {
                    new ProductRequestInfo { QuotationRequestId = quotationRequest.Id, ProductId = "P001", Quantity = 5 },
                    new ProductRequestInfo { QuotationRequestId = quotationRequest.Id, ProductId = "P002", Quantity = 3 }
                });
                context.SaveChanges();
            }

            if (!context.QuotationResponses.Any())
            {
                var quotationResponse = new QuotationResponse
                {
                    RequestId = 1, // Use int value
                    ResponseDate = new DateTime(2025, 7, 23, 17, 54, 0, DateTimeKind.Utc) // July 23, 2025, 05:54 PM UTC
                };
                context.QuotationResponses.Add(quotationResponse);
                context.SaveChanges();

                context.ProductQuoteInfos.AddRange(new List<ProductQuoteInfo>
                {
                    new ProductQuoteInfo { QuotationResponseId = quotationResponse.Id, ProductId = "P001", PricePerUnit = 110.00m, AvailableQuantity = 6, EstimatedDeliveryDays = 3 },
                    new ProductQuoteInfo { QuotationResponseId = quotationResponse.Id, ProductId = "P002", PricePerUnit = 120.00m, AvailableQuantity = 4, EstimatedDeliveryDays = 5 }
                });
                context.SaveChanges();
            }

            if (!context.OrderConfirmations.Any())
            {
                var orderConfirmation = new OrderConfirmation
                {
                    // OrderId will be auto-generated
                    RequestId = 1, // Use int value
                    ConfirmationDate = new DateTime(2025, 7, 23, 17, 58, 0, DateTimeKind.Utc),
                    Status = "Confirmed"
                };
                context.OrderConfirmations.Add(orderConfirmation);
                context.SaveChanges();
            }
        }
    }
}