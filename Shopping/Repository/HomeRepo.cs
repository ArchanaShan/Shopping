using Microsoft.AspNetCore.Mvc.Rendering;
using Shopping.Models;

namespace Shopping.Repository
{
    public class HomeRepo : IHomeRepo
    {
        private readonly ShoppingContext _dbContext;

        public HomeRepo(ShoppingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SelectListItem> GetItemsForDropdown()
        {
            List<ProductsCategory> lstProductsCategories = _dbContext.ProductsCategories.ToList();
            var selectListItems = lstProductsCategories.Select(item => new SelectListItem
            {
                Value = item.ProductCategoryId.ToString(),
                Text = item.Name
            }).ToList();

            return selectListItems;
        }

        public List<Product> GetItemsFromProducts(int selectedItemId)
        {
            List<Product> lstProduct = _dbContext.Products.Where(x => x.ProductCategoryId == selectedItemId).ToList();
            return lstProduct;
        }
    }
}
