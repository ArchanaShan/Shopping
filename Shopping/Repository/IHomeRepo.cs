using Microsoft.AspNetCore.Mvc.Rendering;
using Shopping.Models;

namespace Shopping.Repository
{
    public interface IHomeRepo
    {
        List<SelectListItem> GetItemsForDropdown();
        List<Product> GetItemsFromProducts(int selectedItemId);
    }
}
