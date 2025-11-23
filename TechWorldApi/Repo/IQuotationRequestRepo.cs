using System.Collections.Generic;
using System.Threading.Tasks;
using TechWorld.Models;

namespace TechWorld.Repo
{
    public interface IQuotationRequestRepo
    {
        Task SaveAsync(QuotationRequest quotationRequest);
        Task SaveAsync(List<ProductRequestInfo> productRequestInfos);
        Task<QuotationRequest> GetByIdAsync(int id);
        Task UpdateAsync(QuotationRequest quotationRequest);
        Task DeleteAsync(QuotationRequest quotationRequest);
        Task<List<QuotationRequest>> GetAllAsync(); // Added for GET all
    }
}