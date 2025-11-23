using AutoMapper;
using ElectroComApi.DTO;
using ElectroComApi.Models;

namespace ElectroComApi.Profiles
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
