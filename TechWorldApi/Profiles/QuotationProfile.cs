using AutoMapper;
using TechWorld.DTO;
using TechWorld.Models;

namespace TechWorldApi.Profiles
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
