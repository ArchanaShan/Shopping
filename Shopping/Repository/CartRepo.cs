using Microsoft.EntityFrameworkCore;
using Shopping.Common;
using Shopping.Models;

namespace Shopping.Repository
{
    public class CartRepo : ICartRepo
    {
        private readonly ShoppingContext _context;
        private readonly StoredProcedureCall _sp;
        public CartRepo(ShoppingContext context, StoredProcedureCall sp)
        {
            _context = context;
            _sp = sp;
        }
        public async Task<int> DecreaseQuantity(int productId, int orderId)
        {
            using var transaction = _context.Database.BeginTransaction();
            var currentQuantity = _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            if (currentQuantity is null)
            {
                throw new Exception("No item in cart");

            }
            else
            {
                _sp.DecreaseOrderDetail(currentQuantity.OrderId, currentQuantity.ProductId, currentQuantity.Quantity);
            }
            int quan = await UpdatedQuantity(productId, orderId);
            return quan;
        }

        public async Task<int> DeleteItem(int productId, int orderId)
        {
            using var transaction = _context.Database.BeginTransaction();
            var currentQuantity = _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            if (currentQuantity is null)
            {
                throw new Exception("No item in cart");

            }
            _sp.DecreaseOrderDetail(currentQuantity.OrderId, currentQuantity.ProductId, currentQuantity.Quantity);
            int quan = await UpdatedQuantity(productId, orderId);
            return quan;
        }

        public async Task<int> IncreaseQuantity(int productId, int orderId)
        {
            var currentQuantity = _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            if (currentQuantity != null)
            {
                currentQuantity.Quantity = currentQuantity.Quantity + 1;
                _sp.UpdateOrderDetail(currentQuantity.OrderId, currentQuantity.ProductId, currentQuantity.Quantity);
            }
            int quan = await UpdatedQuantity(productId, orderId);
            return quan;
        }

        public async Task<int> UpdatedQuantity(int productId, int orderId)
        {
            var updateQuantity = await _context.OrderDetails.FirstOrDefaultAsync(y => y.OrderId == orderId && y.ProductId == productId);
            if (updateQuantity == null)
            {
                return 0;
            }
            return updateQuantity.Quantity;
        }

    }
}
