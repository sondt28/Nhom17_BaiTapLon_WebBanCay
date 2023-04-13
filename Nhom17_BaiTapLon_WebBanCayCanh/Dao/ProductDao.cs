using Microsoft.Data.SqlClient;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Data;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Dao
{
    public class ProductDao
    {
        public static void CreateProduct(IConfiguration configuration, Product product)
        {
            using(SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_createProduct", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Name", product.Name);
                cmd.Parameters.AddWithValue("Description", product.Description);
                cmd.Parameters.AddWithValue("Availability", product.Availability);
                cmd.Parameters.AddWithValue("Image", product.Image);
                cmd.Parameters.AddWithValue("CategoryId", product.CategoryId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void GetProducts(IConfiguration configuration)
        {
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                
            }
        }
    }
}
