namespace Shopping.Repository
{
    public interface IProductRepo
    {
        Task<int> AddItem(int productId, int quantity, string userId);
        Task<int> Cartcount(string userId);
        int CreateOrderId(string userId);
        int StartNewOrder();
    }
}
