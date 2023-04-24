using Microsoft.Data.SqlClient;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Data;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Dao
{
    public class CategoryDao
    {
        public static List<Category> GetCategoiesSelection(IConfiguration configuration)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getCategoriesSelection", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataTable);
            }
            List<Category> categories = ConvertDataTableToCategoriesWithoutParentCategory(dataTable);

            return categories;
        }
        public static List<Category> GetCategoriesWithoutProduct(IConfiguration configuration)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getCategoriesWithoutProduct", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataTable);
            }
            List<Category> categories = ConvertDataTableToCategoriesWithoutParentCategory(dataTable);

            return categories;
        }

        public static List<Category> GetCategoies(IConfiguration configuration)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getCategories", sqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataTable);
            }
            List<Category> categories = ConvertDataTableToCategories(dataTable);

            return categories;
        }
        public static void CreateCategory(IConfiguration configuration, Category category)
        {
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_createCategory", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Name", category.Name);
                cmd.Parameters.AddWithValue("ParentCategoryId", category.ParentCategoryId);
                cmd.ExecuteNonQuery();
            }
        }
        private static List<Category> ConvertDataTableToCategoriesWithoutParentCategory(DataTable dataTable)
        {
            List<Category> categories = new List<Category>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Category category = new Category();
                category.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                category.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                categories.Add(category);
            }

            return categories;
        }
        private static List<Category> ConvertDataTableToCategories(DataTable dataTable)
        {
            List<Category> categories = new List<Category>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Category category = new Category();
                category.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                category.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                Category parentCategory = new Category();

                if (dataTable.Rows[i]["ParentCategoryId"] != DBNull.Value)
                {
                    parentCategory.Id = Convert.ToInt32(dataTable.Rows[i]["ParentCategoryId"]);
                    parentCategory.Name = Convert.ToString(dataTable.Rows[i]["ParentCategoryName"]);
                }
                else
                {
                    category.ParentCategoryId = 0;
                }
                category.ParentCategory = parentCategory;
                categories.Add(category);
            }
            return categories;
        }
    }
}
