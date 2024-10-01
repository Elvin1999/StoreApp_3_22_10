namespace ProductService.Services
{
    public interface IProductExtentionServices
    {
        Task<string> GetProductImageAsync(int productId);
    }
}
