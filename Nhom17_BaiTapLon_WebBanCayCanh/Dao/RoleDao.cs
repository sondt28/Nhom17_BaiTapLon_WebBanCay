using Microsoft.Data.SqlClient;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Data;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Dao
{
    public class RoleDao
    {
        public static RoleViewModel GetRoles(IConfiguration configuration)
        {
            DataTable dataTable = new DataTable();
            using(SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("sp_getRoles", sqlConnection);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.Fill(dataTable);
            }
            List<Role> roles = new List<Role>();
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                Role role = new Role();
                role.Id = Convert.ToString(dataTable.Rows[i]["Id"]);
                role.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                roles.Add(role);
            }
            RoleViewModel roleViewModel = new RoleViewModel();
            roleViewModel.Roles = roles;

            return roleViewModel;
        }
        public static List<Role> GetRoles1(IConfiguration configuration)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("sp_getRoles", sqlConnection);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.Fill(dataTable);
            }
            List<Role> roles = new List<Role>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Role role = new Role();
                role.Id = Convert.ToString(dataTable.Rows[i]["Id"]);
                role.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                roles.Add(role);
            }

            return roles;
        }
        public static RoleViewModel getRole(IConfiguration configuration, string id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("sp_getRole", sqlConnection);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("Id", id);
              
                sqlDataAdapter.Fill(dataTable);
            }
            RoleViewModel role = new RoleViewModel();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                role.Id = Convert.ToString(dataTable.Rows[i]["Id"]);
                role.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                role.ConcurrencyStamp = Convert.ToString(dataTable.Rows[i]["ConcurrencyStamp"]);
            }

            return role;
        }
        public static void CreateRole(IConfiguration configuration, Role role)
        {
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("sp_createRole", sqlConnection);
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.SelectCommand.Parameters.AddWithValue("Name", role.Name);
            }
        }
        
    }
}
