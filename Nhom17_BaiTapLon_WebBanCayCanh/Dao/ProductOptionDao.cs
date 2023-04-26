using Microsoft.Data.SqlClient;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Data;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Dao
{
    public class ProductOptionDao
    {
        public static void CreateProductOption(IConfiguration configuration, ProductOption productOption)
        {
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_createProductOption", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Value", productOption.Value);
                cmd.Parameters.AddWithValue("Price", productOption.Price);
                cmd.Parameters.AddWithValue("Image", productOption.Image);
                cmd.Parameters.AddWithValue("StockQuantity", productOption.StockQuantity);
                cmd.Parameters.AddWithValue("ProductId", productOption.ProductId);

                cmd.ExecuteNonQuery();
            }
        }
        public static ProductOptionViewModel GetProductOptions(IConfiguration configuration)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getProductOptions", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataTable);
            }
            List<ProductOption> productOptions = ConvertDataTableToProductOptions(dataTable);
            ProductOptionViewModel model = new ProductOptionViewModel();
            model.ProductOptions = productOptions;
            return model;
        }
        public static ProductOption GetProductOption(IConfiguration configuration, int id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getProductOptionById", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("Id", id);
                adapter.Fill(dataTable);
            }
            ProductOption productOption = ConvertDataTableToProductOption(dataTable);
            return productOption;
        }
        public static void UpdateProductOption(IConfiguration configuration, ProductOption producOption)
        {
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_updateProductOption", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", producOption.Id);
                cmd.Parameters.AddWithValue("Value", producOption.Value);
                cmd.Parameters.AddWithValue("Price", producOption.Price);
                cmd.Parameters.AddWithValue("StockQuantity", producOption.StockQuantity);
                cmd.Parameters.AddWithValue("Image", producOption.Image);
                cmd.Parameters.AddWithValue("ProductId", producOption.ProductId);
                cmd.ExecuteNonQuery();
            }
        }
        public static void DeleteProductOption(IConfiguration configuration, int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_deleteProductOption", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", id);
                cmd.ExecuteNonQuery();
            }
        }
        private static ProductOption ConvertDataTableToProductOption(DataTable dataTable)
        {
            ProductOption productOption = new ProductOption();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                
                productOption.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                productOption.Value = Convert.ToString(dataTable.Rows[i]["Value"]);
                productOption.Image = Convert.ToString(dataTable.Rows[i]["Image"]);
                productOption.StockQuantity = Convert.ToInt32(dataTable.Rows[i]["StockQuantity"]);
                productOption.Price = Convert.ToInt32(dataTable.Rows[i]["Price"]);

                Product product = new Product();
                if (dataTable.Rows[i]["ProductId"] != DBNull.Value)
                {
                    product.Id = Convert.ToInt32(dataTable.Rows[i]["ProductId"]);
                    product.Name = Convert.ToString(dataTable.Rows[i]["ProductName"]);
                }
                else
                {
                    productOption.ProductId = 0;
                }
                productOption.ProductId = product.Id;
                productOption.Product = product;
            }

            return productOption;
        }
        private static List<ProductOption> ConvertDataTableToProductOptions(DataTable dataTable)
        {
            List<ProductOption> productOptions = new List<ProductOption>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                ProductOption productOption = new ProductOption();
                productOption.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                productOption.Value = Convert.ToString(dataTable.Rows[i]["Value"]);

                Product product = new Product();
                if (dataTable.Rows[i]["ProductId"] != DBNull.Value)
                {
                    product.Id = Convert.ToInt32(dataTable.Rows[i]["ProductId"]);
                    product.Name = Convert.ToString(dataTable.Rows[i]["ProductName"]);
                }
                else
                {
                    productOption.ProductId = 0;
                }
                productOption.ProductId = product.Id;
                productOption.Product = product;
                productOptions.Add(productOption);
            }
            return productOptions;
        }
    }
}
