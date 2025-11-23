using Gadget_CentralApi.DTO;
using Gadget_CentralApi.Models;
using AutoMapper;

namespace Gadget_CentralApi.Profiles
{
    public class QuotationResponses: Profile
    {
        public QuotationResponses()
        {
            CreateMap<QuotationResponse, QuotationResponseDto>();
            CreateMap<QuotationResponseDto, QuotationResponse>();
            CreateMap<ProductQuoteInfo, ProductQuoteInfoReadDto>();
            CreateMap<ProductQuoteInfoReadDto, ProductQuoteInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
