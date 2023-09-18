namespace Shopping.ViewModels
{
    public class CheckOutDetailsViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public int OrderId { get; set; }

        public int TotalAmount { get; set; }

        public List<OrderSummaryViewModel> listOrderDetails { get; set; }
    }
}
