using BusinessModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.IRepositories;

namespace DataAccessLayer.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        string? _connectionString = string.Empty;
        public LoginRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public EmployeeProfileDetails LoginAuthenticationRepository(string LoginName, string Password)
        {

            EmployeeProfileDetails _LoginCredentials = new EmployeeProfileDetails();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("LoGINbYcREDENTIals", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@loginname", LoginName);
                cmd.Parameters.AddWithValue("@password", Password);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    _LoginCredentials.Id = Convert.ToInt32(rdr["Id"]);
                    _LoginCredentials.ReportingManagerName = rdr["ReportingManagerName"].ToString();
                    _LoginCredentials.Email = rdr["Email"].ToString();
                    _LoginCredentials.Mobile = rdr["Mobile"].ToString();
                    _LoginCredentials.City = rdr["Location"].ToString();
                    _LoginCredentials.LoginName = rdr["LoginName"].ToString();
                    _LoginCredentials.Password = rdr["password"].ToString();
                    _LoginCredentials.RoleName = rdr["RoleName"].ToString();
                }
                con.Close();
            }
            return _LoginCredentials;
        }
    }
}
