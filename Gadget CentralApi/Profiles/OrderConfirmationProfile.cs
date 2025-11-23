using AutoMapper;
using Gadget_CentralApi.DTO;
using Gadget_CentralApi.Models;

namespace Gadget_CentralApi.Profiles
{
    public class OrderConfirmationProfile: Profile
    {
        public OrderConfirmationProfile()
        {
            CreateMap<OrderConfirmation, OrderConfirmationDto>();
            CreateMap<OrderConfirmationDto, OrderConfirmation>();
            CreateMap<ConfirmedProduct, ConfirmedProductDto>();
            CreateMap<ConfirmedProductDto, ConfirmedProduct>();
        }
    }
}
