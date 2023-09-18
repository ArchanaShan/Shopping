using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Repository;
using System.Diagnostics;

namespace Shopping.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShoppingContext _context;
        private readonly IHomeRepo _homeRepo;

        public HomeController(ILogger<HomeController> logger, ShoppingContext context, IHomeRepo homeRepo)
        {
            _logger = logger;
            _context = context;
            _homeRepo = homeRepo;
        }

        public IActionResult Index()
        {
            var productcategory = _homeRepo.GetItemsForDropdown();
            List<Product> lstProduct = _context.Products.ToList();
            ViewBag.ItemsForDropdown = productcategory;

            return View(lstProduct);
        }

        public IActionResult GetItemCategoryList(int selectedItemId)
        {
            var itemList = _homeRepo.GetItemsFromProducts(selectedItemId);
            return PartialView("_ProductList", itemList);
        }
        [HttpPost]
        public IActionResult Search(string searchTerm)
        {
            List<Product> listProduct = _context.Products.Where(x => x.Name == searchTerm).ToList();
            return PartialView("_ProductList", listProduct);
        }

    }
}