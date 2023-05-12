using Microsoft.Data.SqlClient;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;
using System.Data;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Dao
{
    public class AddressDao
    {
        public static void SaveAddress(IConfiguration configuration, Address address) {
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("sp_createAddress", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserId", address.UserId);
                cmd.Parameters.AddWithValue("Name", address.Name);
                cmd.Parameters.AddWithValue("PhoneNumber", address.PhoneNumber);
                cmd.Parameters.AddWithValue("AddressDetails", address.AddressDetails);
                cmd.Parameters.AddWithValue("City", address.City);
                cmd.Parameters.AddWithValue("District", address.District);
                cmd.Parameters.AddWithValue("SubDistrict", address.SubDistrict);
                cmd.Parameters.AddWithValue("Note", address.Note);

                cmd.ExecuteNonQuery();
            }
        }
        public static Address GetAddressByUserId(IConfiguration configuration, string userId)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("sp_getAddressByUserId", sqlConnection);
                adapter.SelectCommand.Parameters.AddWithValue("UserId", userId);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataTable);
            }
            Address address = ConvertDataTableToAddress(dataTable);

            return address;
        }
        private static Address ConvertDataTableToAddress(DataTable dataTable)
        {
            Address address = new Address();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                address.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                address.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                address.PhoneNumber = Convert.ToString(dataTable.Rows[i]["PhoneNumber"]);
                address.AddressDetails = Convert.ToString(dataTable.Rows[i]["AddressDetails"]);
                address.City = Convert.ToString(dataTable.Rows[i]["City"]);
                address.District = Convert.ToString(dataTable.Rows[i]["District"]);
                address.SubDistrict = Convert.ToString(dataTable.Rows[i]["SubDistrict"]);
                address.Note = Convert.ToString(dataTable.Rows[i]["Note"]);
                address.UserId = Convert.ToString(dataTable.Rows[i]["UserId"]);
            }
            return address;
        }
    }
}
