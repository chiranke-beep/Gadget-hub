using AutoMapper;
using ElectroComApi.DTO;
using ElectroComApi.Models;

namespace ElectroComApi.Profiles
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
