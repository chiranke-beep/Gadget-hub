using AutoMapper;
using Gadget_CentralApi.DTO;
using Gadget_CentralApi.Models;

namespace Gadget_CentralApi.Profiles
{
    public class QuotationProfile: Profile
    {
        public QuotationProfile()
        {
            // QuotationRequest mappings
            CreateMap<QuotationRequest, QuotationRequestDto>();
            CreateMap<QuotationRequestDto, QuotationRequest>();
            
            // ProductRequestInfo mappings
            CreateMap<ProductRequestInfo, ProductRequestDto>();
            CreateMap<ProductRequestDto, ProductRequestInfo>();
            
            // ProductQuoteInfo mappings
            CreateMap<ProductQuoteInfo, ProductQuoteInfoReadDto>();
            CreateMap<ProductQuoteInfoReadDto, ProductQuoteInfo>();
            
            // QuotationResponse mappings
            CreateMap<QuotationResponse, QuotationResponseDto>();
            CreateMap<QuotationResponseDto, QuotationResponse>();
        }
    }
}
