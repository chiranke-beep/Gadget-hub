using AutoMapper;
using ElectroComApi.DTO;
using ElectroComApi.Models;

namespace ElectroComApi.Profiles
{
    public class QuotationResponses: Profile
    {
        public QuotationResponses()
        {
            CreateMap<QuotationResponse, QuotationResponseDto>();
            CreateMap<QuotationResponseDto, QuotationResponse>();
            CreateMap<ProductQuoteInfo, ProductQuoteInfoReadDto>();
            // Prevent mapping Id from DTO to entity
            CreateMap<ProductQuoteInfoReadDto, ProductQuoteInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }

    }
}
