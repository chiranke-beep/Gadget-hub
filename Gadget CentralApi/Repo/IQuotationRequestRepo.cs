using Gadget_CentralApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gadget_CentralApi.Repo
{
    public interface IQuotationRequestRepo
    {
        Task SaveAsync(QuotationRequest quotationRequest);
        Task SaveAsync(List<ProductRequestInfo> productRequestInfos);
        Task<QuotationRequest> GetByIdAsync(int id);
        Task UpdateAsync(QuotationRequest quotationRequest);
        Task DeleteAsync(QuotationRequest quotationRequest);
        Task<List<QuotationRequest>> GetAllAsync();
    }
}