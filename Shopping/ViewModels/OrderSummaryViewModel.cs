using Shopping.Models;

namespace Shopping.ViewModels
{
    public class OrderSummaryViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ICollection<OrderDetail> OrderDetailList { get; set; }
    }
}
