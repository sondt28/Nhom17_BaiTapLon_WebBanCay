using Microsoft.Data.SqlClient;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Data;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Dao
{
    public class UserDao
    {
        public static UserViewModel getUserRoles(IConfiguration configuration)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("sp_getUsersWithRole", sqlConnection);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.Fill(dataTable);
            }
            List<UserRole> userRoles = new List<UserRole>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                UserRole userRole = new UserRole();
                userRole.Id = Convert.ToString(dataTable.Rows[i]["Id"]);
                userRole.Email = Convert.ToString(dataTable.Rows[i]["Email"]);
                userRole.RoleName = Convert.ToString(dataTable.Rows[i]["RoleName"]);
                userRoles.Add(userRole);
            }
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Users = userRoles;

            return userViewModel;
        }
        public static UserViewModel getUser(IConfiguration configuration, string id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("sp_getUserAndRole", sqlConnection);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("Id", id);
                sqlDataAdapter.Fill(dataTable);
            }
            UserViewModel userRole = new UserViewModel();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                userRole.Id = Convert.ToString(dataTable.Rows[i]["Id"]);
                userRole.Email = Convert.ToString(dataTable.Rows[i]["Email"]);
                userRole.RoleName = Convert.ToString(dataTable.Rows[i]["RoleName"]);
                userRole.RoleId = Convert.ToString(dataTable.Rows[i]["RoleId"]);
            }
            return userRole;
        }
    }
}
