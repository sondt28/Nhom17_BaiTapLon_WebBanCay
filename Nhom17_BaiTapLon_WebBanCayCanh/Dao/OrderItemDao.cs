using Microsoft.Data.SqlClient;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Data;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Dao
{
    public class OrderItemDao
    {
        public static void SaveOrderItem(IConfiguration configuration, OrderItem orderItem)
        {
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_createOrderItem", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Quantity", orderItem.Quantity);
                cmd.Parameters.AddWithValue("Price", orderItem.Price);
                cmd.Parameters.AddWithValue("ProductId", orderItem.ProductId);
                cmd.Parameters.AddWithValue("ProductOptionId", orderItem.ProductOptionId);
                cmd.Parameters.AddWithValue("OrderId", orderItem.OrderId);
                cmd.ExecuteNonQuery();
            }
        }
        public static Order GetTotalOfOrderItems(IConfiguration configuration, string userId)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_countOrderItemOfCart", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("UserId", userId);

                adapter.Fill(dataTable);
            }
            Order order = new Order();
            if (dataTable.Rows.Count > 0)
            {
                order.Count = Convert.ToInt32(dataTable.Rows[0]["TotalOrderItems"]);
                order.Id = Convert.ToInt32(dataTable.Rows[0]["OrderId"]);
            }

            return order;
        }
        public static void DeleteOrderItem(IConfiguration configuration, int orderItemId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_deleteOrderItem", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("OrderItemId", orderItemId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
