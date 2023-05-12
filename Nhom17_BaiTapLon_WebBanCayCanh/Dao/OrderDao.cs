using Microsoft.Data.SqlClient;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Configuration;
using System.Data;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Dao
{
    public class OrderDao
    {
        public static void CreateOrder(IConfiguration configuration, Order order)
        {
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_saveOrder", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserId", order.UserId);
                cmd.Parameters.AddWithValue("Status", order.Status);
                cmd.ExecuteNonQuery();
            }
        }
        public static Order GetOrder(IConfiguration configuration, string userId)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getOrderByUserId", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("UserId", userId);
                adapter.Fill(dataTable);
            }
            Order order = ConvertDataTableToOrder(dataTable);

            return order;
        }
        public static Order GetOrderDetails(IConfiguration configuration, int orderId)
        {
            DataTable dtbl = new DataTable();
            var countParameter = new SqlParameter("@TotalPrice", SqlDbType.Decimal);
            countParameter.Direction = ParameterDirection.Output;
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getOrderDetails", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("OrderId", orderId);
                adapter.SelectCommand.Parameters.Add(countParameter);
                adapter.Fill(dtbl);
            }
            decimal totalPrice = 0;
            if (countParameter.Value != DBNull.Value)
            {
                totalPrice = (decimal)countParameter.Value;
            }
            
            List<OrderItem> orderItems = ConvertDataTableToOrders(dtbl);
            Order order = new Order();
            order.TotalAmount = totalPrice;
            order.Id = orderId;
            order.OrderItems = orderItems;
            return order;
        }
        private static List<OrderItem> ConvertDataTableToOrders(DataTable dataTable)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.Id = Convert.IsDBNull(dataTable.Rows[i]["OrderItemId"]) ? 0 : Convert.ToInt32(dataTable.Rows[i]["OrderItemId"]);
                orderItem.Quantity = Convert.IsDBNull(dataTable.Rows[i]["Quantity"]) ? 0 : Convert.ToInt32(dataTable.Rows[i]["Quantity"]);
                orderItem.Price = Convert.IsDBNull(dataTable.Rows[i]["Price"]) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["Price"]);

                Product product = new Product()
                {
                    Id = Convert.IsDBNull(dataTable.Rows[i]["ProductId"]) ? 0 : Convert.ToInt32(dataTable.Rows[i]["ProductId"]),
                    Name = Convert.IsDBNull(dataTable.Rows[i]["ProductName"]) ? null : Convert.ToString(dataTable.Rows[i]["ProductName"]),
                    Image = Convert.IsDBNull(dataTable.Rows[i]["ProductImage"]) ? null : Convert.ToString(dataTable.Rows[i]["ProductImage"]),
                    
                };
                orderItem.Product = product;

                if (dataTable.Rows[i]["ProductOptionId"] != DBNull.Value)
                {
                    ProductOption productOption = new ProductOption();
                    productOption.Id = Convert.IsDBNull(dataTable.Rows[i]["ProductId"]) ? 0 : Convert.ToInt32(dataTable.Rows[i]["ProductOptionId"]);
                    productOption.Value = Convert.IsDBNull(dataTable.Rows[i]["ProductOptionValue"]) ? null : Convert.ToString(dataTable.Rows[i]["ProductOptionValue"]);
                    productOption.Image = Convert.IsDBNull(dataTable.Rows[i]["ProductImage"]) ? null : Convert.ToString(dataTable.Rows[i]["ProductOptionImage"]);
                    orderItem.ProductOption = productOption;
                } else
                {
                    orderItem.ProductOptionId = 0;
                }
                orderItems.Add(orderItem);
            }
            return orderItems;
        }
        private static Order ConvertDataTableToOrder(DataTable dataTable)
        {
            Order order = new Order();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                order.Id = Convert.IsDBNull(dataTable.Rows[i]["Id"]) ? 0 : Convert.ToInt32(dataTable.Rows[i]["Id"]);
                order.Date = Convert.IsDBNull(dataTable.Rows[i]["Date"]) ? null : Convert.ToDateTime(dataTable.Rows[i]["Date"]);
                order.TotalAmount = Convert.IsDBNull(dataTable.Rows[i]["TotalAmount"]) ? 0 : Convert.ToDecimal(dataTable.Rows[i]["TotalAmount"]);
                string statusString = Convert.ToString(dataTable.Rows[i]["Status"]);
                OrderStatus status;
                if (Enum.TryParse(statusString, out status))
                {
                    order.Status = status;
                }
            }
            return order;
        }
    }
}
