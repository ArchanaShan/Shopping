using Microsoft.Data.SqlClient;
using Shopping.Models;

namespace Shopping.Common
{
    public class StoredProcedureCall
    {
        private readonly IConfiguration _configuration;
        private readonly ShoppingContext _context;

        public StoredProcedureCall(IConfiguration configuration, ShoppingContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public void UpdateOrderDetail(int orderId, int productId, int quantity)
        {

            string connectionstring = _configuration.GetConnectionString("ShoppingContext");
            string errorMessage = null;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateOrderDetail"))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred: " + ex.Message;
            }
        }
        public void DecreaseOrderDetail(int orderId, int productId, int quantity)
        {
            string connectionstring = _configuration.GetConnectionString("ShoppingContext");
            string errorMessage = null;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("DecreaseOrderDetail"))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred: " + ex.Message;
            }
        }
    }
}
