using Microsoft.Data.SqlClient;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Collections.Generic;
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
                cmd.Parameters.AddWithValue("StockQuantity", product.StockQuantity);
                cmd.Parameters.AddWithValue("Price", product.Price);
                cmd.Parameters.AddWithValue("CategoryId", product.CategoryId);
                cmd.ExecuteNonQuery();
            }
        }
        public static void UpdateProduct(IConfiguration configuration, Product product)
        {
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_updateProduct", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", product.Id);
                cmd.Parameters.AddWithValue("Name", product.Name);
                cmd.Parameters.AddWithValue("Description", product.Description);
                cmd.Parameters.AddWithValue("Availability", product.Availability);
                cmd.Parameters.AddWithValue("Image", product.Image);
                cmd.Parameters.AddWithValue("StockQuantity", product.StockQuantity);
                cmd.Parameters.AddWithValue("Price", product.Price);
                cmd.Parameters.AddWithValue("CategoryId", product.CategoryId);
                cmd.ExecuteNonQuery();
            }
        }
        public static ProductViewModel GetProducts(IConfiguration configuration)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getProductsWithCategory", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataTable);
            }
            List<Product> products = ConvertDataTableToProducts(dataTable);
            ProductViewModel model = new ProductViewModel();
            model.Products = products;
            return model;
        }
        public static Product GetProduct(IConfiguration configuration, int id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getProductById", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("Id", id);
                adapter.Fill(dataTable);
            }
            Product product = ConvertDataTableToProduct(dataTable);

            return product;
        }
        public static PaginatedList<Product> GetProductPages(IConfiguration configuration, int pageSize, int pageIndex, string searchTerm, int categoryId)
        {
            DataTable dtbl = new DataTable();
            var countParameter = new SqlParameter("@Count", SqlDbType.Int);
            countParameter.Direction = ParameterDirection.Output;
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getProductPages", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("SearchTerm", searchTerm);
                adapter.SelectCommand.Parameters.AddWithValue("PageIndex", pageIndex);
                adapter.SelectCommand.Parameters.AddWithValue("PageSize", pageSize);
                adapter.SelectCommand.Parameters.AddWithValue("CategoryId", categoryId);
                adapter.SelectCommand.Parameters.Add(countParameter);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dtbl);
            }
            int countValue = (int)countParameter.Value;

            List<Product> products = ConvertDataTableToProductPages(dtbl);

            PaginatedList<Product> page = new PaginatedList<Product>(products, countValue, pageIndex, pageSize);
            return page;
        }
        public static List<Product> GetProducstSelect(IConfiguration configuration)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getProductSelects", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataTable);
            }
            List<Product> products = ConvertDataTableToProductsSelect(dataTable);

            return products;
        }
        private static List<Product> ConvertDataTableToProductPages(DataTable dataTable)
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                product.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                product.Image = Convert.ToString(dataTable.Rows[i]["Image"]);
                product.Price = Math.Round(Convert.ToDecimal(dataTable.Rows[i]["Price"]), 0);

                products.Add(product);
            }
            return products;
        }

        private static List<Product> ConvertDataTableToProducts(DataTable dataTable)
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                product.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                product.Description = Convert.ToString(dataTable.Rows[i]["Description"]);
                product.Availability = Convert.ToBoolean(dataTable.Rows[i]["Availability"]);
                product.Image = Convert.ToString(dataTable.Rows[i]["Image"]);
                product.StockQuantity = Convert.ToInt32(dataTable.Rows[i]["StockQuantity"]);
                product.Price = Convert.ToDecimal(dataTable.Rows[i]["Price"]);

                Category category = new Category();
                if (dataTable.Rows[i]["CategoryId"] != DBNull.Value)
                {
                    category.Id = Convert.ToInt32(dataTable.Rows[i]["CategoryId"]);
                    category.Name = Convert.ToString(dataTable.Rows[i]["CategoryName"]);
                } else
                {
                    product.CategoryId = 0;
                }

                product.Category = category;
                product.CategoryId = category.Id;
                products.Add(product);
            }
            return products;
        }
        private static List<Product> ConvertDataTableToProductsSelect(DataTable dataTable)
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                product.Name = Convert.ToString(dataTable.Rows[i]["Name"]);

                products.Add(product);
            }
            return products;
        }

        private static Product ConvertDataTableToProduct(DataTable dataTable)
        {
            Product product = new Product();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                product.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                product.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                product.Description = Convert.ToString(dataTable.Rows[i]["Description"]);
                product.Availability = Convert.ToBoolean(dataTable.Rows[i]["Availability"]);
                product.Image = Convert.ToString(dataTable.Rows[i]["Image"]);
                product.StockQuantity = Convert.ToInt32(dataTable.Rows[i]["StockQuantity"]);
                product.Price = Convert.ToDecimal(dataTable.Rows[i]["Price"]);

                Category category = new Category();
                if (dataTable.Rows[i]["CategoryId"] != DBNull.Value)
                {
                    category.Id = Convert.ToInt32(dataTable.Rows[i]["CategoryId"]);
                    category.Name = Convert.ToString(dataTable.Rows[i]["CategoryName"]);
                }
                else
                {
                    product.CategoryId = 0;
                }

                product.Category = category;
            }
            return product;
        }
    }
}
