using BusinessModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.IRepositories;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        string? _connectionString = string.Empty;
        //public readonly IConfiguration _IConfiguration;

        public DashboardRepository(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("DefaultConnection");
        }

        public List<EmployeeProfileDetails> GetAllEmployeesRepository()
        {
            List<EmployeeProfileDetails> lstResourceDetails = new List<EmployeeProfileDetails>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EmployeeProfileDetails resourceDetails = new EmployeeProfileDetails();
                    resourceDetails.Id = Convert.ToInt32(reader["Id"]);
                    resourceDetails.ResourceName = reader["ResourceName"].ToString();
                    resourceDetails.Email = reader["Email"].ToString();
                    resourceDetails.Mobile = reader["Mobile"].ToString();
                    resourceDetails.RoleName = reader["RoleName"].ToString();
                    resourceDetails.City = reader["Location"].ToString();
                    resourceDetails.ReportingManagerName = reader["ReportingManagerName"].ToString();
                    resourceDetails.JoiningDate = reader["JoiningDate"].ToString();

                    lstResourceDetails.Add(resourceDetails);
                }
                con.Close();
            }
            return lstResourceDetails;
        }

        public List<EmployeeProfileDetails> GetAllReportingManagersRepository()
        {
            List<EmployeeProfileDetails> resourcelist = new List<EmployeeProfileDetails>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TC_GetReportingManagers", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeProfileDetails resource = new EmployeeProfileDetails();
                    resource.ReportingManager = (int)reader["ReportingManager"];
                    resource.ReportingManagerName = reader["ReportingManagerName"].ToString();
                    resourcelist.Add(resource);
                }
                con.Close();
            }

            return resourcelist;
        }
    }
}
