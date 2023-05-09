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
