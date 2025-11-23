namespace Gadget_CentralApi.Services
{
    public interface IInventoryService
    {
        bool IsProductAvailable(string productId, int quantity);
    }
}