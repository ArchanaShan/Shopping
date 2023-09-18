namespace Shopping.Repository
{
    public interface ICartRepo
    {
        Task<int> IncreaseQuantity(int productId, int orderId);
        Task<int> UpdatedQuantity(int productId, int orderId);
        Task<int> DecreaseQuantity(int productId, int orderId);
        Task<int> DeleteItem(int productId, int orderId);
    }
}
