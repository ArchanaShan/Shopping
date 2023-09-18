using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Repository;
using Shopping.ViewModels;

namespace Shopping.Controllers
{
    [Authorize(Policy = "RequireLoggedIn")]
    public class CartController : Controller
    {
        private const string SessionOrderId = "CurrentOrderId";
        private readonly ShoppingContext _context;
        private readonly ICartRepo _cartRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ShoppingContext context, ICartRepo cartRepo, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _cartRepo = cartRepo;
            _userManager = userManager;
        }
        public IActionResult OrderSummary()
        {
            var orderId = HttpContext.Session.GetInt32(SessionOrderId);

            var summaryDetails = from product in _context.Products
                                 join orderDetail in _context.OrderDetails on product.ProductId equals orderDetail.ProductId
                                 where orderDetail.OrderId == orderId
                                 select new OrderSummaryViewModel
                                 {
                                     ProductId = product.ProductId,
                                     Name = product.Name,
                                     Image = product.Image,
                                     Price = product.Price,
                                     Quantity = orderDetail.Quantity
                                 };
            UpdatedTotalAmount();
            return View(summaryDetails);
        }
        [HttpPost]
        public async Task<IActionResult> IncreaseItem(int productId, int redirect = 0)
        {
            var orderId = HttpContext.Session.GetInt32(SessionOrderId);
            int quantity = await _cartRepo.IncreaseQuantity(productId, orderId.Value);
            if (redirect == 0)
                return Ok(quantity);
            return RedirectToAction("Cart", "OrderSummary");
        }
        public async Task<IActionResult> DecreaseItem(int productId, int redirect = 0)
        {
            var orderId = HttpContext.Session.GetInt32(SessionOrderId);
            int quantity = await _cartRepo.DecreaseQuantity(productId, orderId.Value);
            if (redirect == 0)
                return Ok(quantity);
            return RedirectToAction("Cart", "OrderSummary");
        }
        [HttpGet]
        public int UpdatedTotalAmount()
        {
            var orderId = HttpContext.Session.GetInt32(SessionOrderId);
            decimal totalAmount = 0;
            IEnumerable<OrderSummaryViewModel> totalItem = from product in _context.Products
                                                           join orderDetail in _context.OrderDetails on product.ProductId equals orderDetail.ProductId
                                                           where orderDetail.OrderId == orderId
                                                           select new OrderSummaryViewModel
                                                           {
                                                               Price = product.Price,
                                                               Quantity = orderDetail.Quantity
                                                           };
            foreach (var item in totalItem)
            {
                totalAmount += item.Price * item.Quantity;
            }
            ViewBag.TotalAmount = totalAmount;
            return (int)totalAmount;
        }
        public async Task<IActionResult> Checkout()
        {
            var userEmail = User.Identity.Name;
            var orderId = HttpContext.Session.GetInt32(SessionOrderId);

            var existingOrder = await _context.Orders.FindAsync(orderId);

            if (existingOrder == null)
            {
                var order = new Order
                {
                    UserId = userEmail,
                    OrderId = orderId.Value,
                    TotalAmount = UpdatedTotalAmount(),
                    CreatedOn = DateTime.Now,
                    CreatedBy = "Admin",
                    ModifiedOn = DateTime.Now,
                    ModifiedBy = "Admin"
                };
                _context.Orders.Add(order);
            }
            else
            {
                existingOrder.TotalAmount = UpdatedTotalAmount();
                existingOrder.ModifiedOn = DateTime.Now;
                existingOrder.ModifiedBy = "Admin";
            }

            var userDetails = await _userManager.FindByEmailAsync(userEmail);

            var orderDetails = from product in _context.Products
                               join orderDetail in _context.OrderDetails on product.ProductId equals orderDetail.ProductId
                               where orderDetail.OrderId == orderId
                               select new OrderSummaryViewModel
                               {
                                   ProductId = product.ProductId,
                                   Name = product.Name,
                                   Image = product.Image,
                                   Price = product.Price,
                                   Quantity = orderDetail.Quantity
                               };

            var checkOutDetail = new CheckOutDetailsViewModel
            {
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Phone = userDetails.PhoneNumber,
                Address = userDetails.Address,
                OrderId = orderId.Value,
                TotalAmount = UpdatedTotalAmount(),
                listOrderDetails = orderDetails.ToList()
            };
            await _context.SaveChangesAsync();

            return View(checkOutDetail);
        }
    }
}
