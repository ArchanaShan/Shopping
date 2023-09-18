using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Repository;

namespace Shopping.Controllers
{
    [Authorize(Policy = "RequireLoggedIn")]
    public class ProductController : Controller
    {
        private readonly IProductRepo _productRepo;
        public ProductController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpPost]
        public async Task<IActionResult> AddDataToOrderDetail(int productId, int quantity = 1, int redirect = 0)
        {
            string userId = User.Identity.Name;
            int cartCount = 0;
            if (userId != null)
            {
                cartCount = await _productRepo.AddItem(productId, quantity, userId);
            }
            if (redirect == 0)
                return Ok(cartCount);

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> GetTotalItemInCart()
        {
            string userId = User.Identity.Name;
            int cartItem = 0;
            cartItem = await _productRepo.Cartcount(userId);

            return Ok(cartItem);
        }
    }
}
