using Microsoft.EntityFrameworkCore;
using Shopping.Common;
using Shopping.Models;

namespace Shopping.Repository
{
    public class ProductRepo : IProductRepo
    {

        private readonly ShoppingContext _context;
        private readonly StoredProcedureCall _sp;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionOrderId = "CurrentOrderId";

        public ProductRepo(ShoppingContext context, StoredProcedureCall sp, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _sp = sp;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddItem(int productId, int quantity, string userId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                int orderId = CreateOrderId(userId);
                var orderDetail = _context.OrderDetails.FirstOrDefault(od => od.ProductId == productId && od.OrderId == orderId);

                if (orderDetail != null)
                {
                    orderDetail.Quantity = orderDetail.Quantity + quantity;
                    _sp.UpdateOrderDetail(orderDetail.OrderId, orderDetail.ProductId, orderDetail.Quantity);
                }
                else
                {
                    orderDetail = new OrderDetail
                    {
                        OrderId = orderId,
                        ProductId = productId,
                        Quantity = quantity,
                        CreatedOn = DateTime.Now,
                        CreatedBy = "Admin",
                        ModifiedOn = DateTime.Now,
                        ModifiedBy = "Admin"
                    };
                    _context.OrderDetails.Add(orderDetail);
                    _context.SaveChanges();
                    transaction.Commit();
                }


            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            int cartcount = await Cartcount(userId);
            return cartcount;
        }

        public async Task<int> Cartcount(string userId)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            var orderId = GetSessionOrderId(httpContext);
            var count = await _context.OrderDetails.CountAsync(x => x.OrderId == orderId);
            return count;
        }

        public int CreateOrderId(string userId)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            var orderId = GetSessionOrderId(httpContext);

            if (orderId < 1)
            {
                orderId = StartNewOrder();
                SetSessionOrderId(httpContext, orderId);
            }

            return orderId;
        }
        public int StartNewOrder()
        {
            var lastOrderId = _context.OrderDetails
           .OrderByDescending(o => o.OrderId).Select(o => o.OrderId)
           .FirstOrDefault();
            if (lastOrderId != 0)
            {
                return lastOrderId + 1;
            }
            else
            {
                return 1;
            }
        }
        public int GetSessionOrderId(HttpContext httpContext)
        {
            return httpContext.Session.GetInt32(SessionOrderId) ?? 0;
        }

        public void SetSessionOrderId(HttpContext httpContext, int orderId)
        {
            httpContext.Session.SetInt32(SessionOrderId, orderId);
        }
    }
}
