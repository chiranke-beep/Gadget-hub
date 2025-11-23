namespace ElectroComApi.Services
{
    public interface IInventoryService
    {
        bool IsProductAvailable(string productId, int quantity);
    }
}