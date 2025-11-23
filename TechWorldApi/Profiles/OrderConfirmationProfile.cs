using AutoMapper;
using TechWorld.DTO;
using TechWorld.Models;

namespace TechWorldApi.Profiles
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
