using BusinessModel;
using DataAccessLayer.IRepositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ManageResourceDetailsRepository : IManageResourceDetailsRepository
    {
        string? _connectionString = string.Empty;
        public ManageResourceDetailsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<EmployeeProfileDetails> GetResourceDetailsRepository()
        {
            List<EmployeeProfileDetails> resourcelist = new List<EmployeeProfileDetails>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TC_GetAllResourceDetails", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeProfileDetails resource = new EmployeeProfileDetails();
                    resource.Id = (int)reader["Id"];
                    resource.DepartmentId = (int)reader["DepartmentId"];
                    resource.DepartmentName = reader["DepartmentName"].ToString();
                    resource.ResourceName = reader["ResourceName"].ToString();
                    resource.RoleId = (int)reader["RoleId"];
                    resource.RoleName = reader["RoleName"].ToString();
                    resource.Mobile = reader["Mobile"].ToString();
                    resource.Email = reader["Email"].ToString();
                    resource.City = reader["City"].ToString();
                    resource.Pincode = Convert.ToInt32(reader["Pincode"]);
                    resource.state = Convert.ToString(reader["State"]);
                    resource.ReportingManager = (int)reader["ReportingManager"];
                    resource.ReportingManagerName = reader["ReportingManagerName"].ToString();
                    resource.CurrentJobStatus = reader["CurrentJobStatus"].ToString();
                    resource.EmployeeStatus = reader["EmployeeStatus"].ToString();
                    resourcelist.Add(resource);
                }
                con.Close();
            }

            return resourcelist;
        }

        public List<DepartmentMaster> GetDepartmentMasterlistRepository()
        {
            List<DepartmentMaster> resourceDetails = new List<DepartmentMaster>();

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllDepartmentMaster", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DepartmentMaster resourcelist = new DepartmentMaster
                    {
                        DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                        DepartmentName = reader["DepartmentName"].ToString()
                    };
                    resourceDetails.Add(resourcelist);
                }
                cn.Close();
            }
            return resourceDetails;
        }

        public List<EmployeeProfileDetails> GetRolesRepository()
        {
            List<EmployeeProfileDetails> resourceDetails = new List<EmployeeProfileDetails>();

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TC_GetAllroles", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeProfileDetails resourcelist = new EmployeeProfileDetails
                    {
                        RoleId = Convert.ToInt32(reader["RoleId"]),
                        RoleName = reader["RoleName"].ToString()
                    };
                    resourceDetails.Add(resourcelist);
                }
                cn.Close();
            }
            return resourceDetails;
        }

        public List<EmployeeProfileDetails> GetFilteredResourceDetailsRepository(string searchText, int? organizationId)
        {
            List<EmployeeProfileDetails> ResourceLiST = GetResourceDetailsRepository();
            List<EmployeeProfileDetails> res = new List<EmployeeProfileDetails>();
            foreach (EmployeeProfileDetails item in ResourceLiST)
            {
                res = ResourceLiST.Where(r => r.ResourceName == searchText || r.OrgId == organizationId).ToList();
            }

            return res;
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

        public int AddResourceDetailsRepository(EmployeeProfileDetails resourceDetails)
        {
            int Success = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("TC_AddResourceDetailsSp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ResourceName", resourceDetails.ResourceName);
                    cmd.Parameters.AddWithValue("@Email", resourceDetails.Email);
                    cmd.Parameters.AddWithValue("@UserRole", resourceDetails.RoleId);
                    cmd.Parameters.AddWithValue("@Mobile", resourceDetails.Mobile);
                    cmd.Parameters.AddWithValue("@City", resourceDetails.City);
                    cmd.Parameters.AddWithValue("@Pincode", resourceDetails.Pincode);
                    cmd.Parameters.AddWithValue("@Description", resourceDetails.Description);
                    cmd.Parameters.AddWithValue("@State", resourceDetails.state);
                    cmd.Parameters.AddWithValue("@ReportingManager", resourceDetails.ReportingManager);
                    cmd.Parameters.AddWithValue("@ImagePath", resourceDetails.ImagePath);
                    cmd.Parameters.AddWithValue("@Department", resourceDetails.DepartmentId);
                    cmd.Parameters.AddWithValue("@EmpCode", resourceDetails.EmployeeCode);

                    // Adding output parameter for the new ID
                    SqlParameter outputParam = new SqlParameter
                    {
                        ParameterName = "@NewId",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Get the newly generated ID
                    Success = (int)cmd.Parameters["@NewId"].Value;

                    conn.Close();
                }
            }
            return Success;
        }

        public int AddResourceCredentialsRepository(int resourceId, string username, string password)
        {
            int Success = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("TC_InUserCredentials", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ResourceId", resourceId);
                    cmd.Parameters.AddWithValue("@LoginName", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    Success = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return Success;
        }

        public EmployeeProfileDetails GetResourceDetailsByResourceIdRepository(int id)
        {
            EmployeeProfileDetails result = new EmployeeProfileDetails();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("TC_GetAllResourceDetailsbYRESOURCEid", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ID", id));

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new EmployeeProfileDetails
                            {
                                ResourceName = reader["ResourceName"].ToString(),
                                Description = reader["Description"].ToString(),
                                RoleId = Convert.ToInt32(reader["UserRole"]),
                                Mobile = reader["Mobile"].ToString(),
                                Email = reader["Email"].ToString(),
                                City = reader["City"].ToString(),
                                Pincode = Convert.ToInt32(reader["Pincode"]),
                                DepartmentId = Convert.ToInt32(reader["Department"]),
                                state = reader["state"].ToString(),
                                EmployeeCode = reader["EmployeeCode"].ToString(),
                                LoginName = reader["LoginName"].ToString(),
                                Password = reader["Password"].ToString(),
                                ImagePath = reader["ImagePath"].ToString()
                            };
                        }
                    }
                }
            }

            return result;
        }

        public int UpdateResourceDetailsRepository(EmployeeProfileDetails resourceDetails, int resourceid)
        {
            int Success = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("TC_ResourceDetailsUpdate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", resourceid);
                    cmd.Parameters.AddWithValue("@ResourceName", resourceDetails.ResourceName);
                    cmd.Parameters.AddWithValue("@UserRole", resourceDetails.RoleId);
                    cmd.Parameters.AddWithValue("@Email", resourceDetails.Email);
                    cmd.Parameters.AddWithValue("@Mobile", resourceDetails.Mobile);
                    cmd.Parameters.AddWithValue("@City", resourceDetails.City);
                    cmd.Parameters.AddWithValue("@Pincode", resourceDetails.Pincode);
                    cmd.Parameters.AddWithValue("@Description", resourceDetails.Description);
                    cmd.Parameters.AddWithValue("@ImagePath", resourceDetails.ImagePath);
                    cmd.Parameters.AddWithValue("@EmpCode", resourceDetails.EmployeeCode);
                    cmd.Parameters.AddWithValue("@State", resourceDetails.state);

                    conn.Open();
                    Success = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return Success;
        }

        public int UpdateManageUserDetailsRepository(EmployeeProfileDetails resourceDetails, int resourceid)
        {
            int Success = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("TC_ManageUserDetailsUpdate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ResourceId", resourceid);
                    cmd.Parameters.AddWithValue("@LoginName", resourceDetails.LoginName);
                    cmd.Parameters.AddWithValue("@Password", resourceDetails.Password);

                    conn.Open();
                    Success = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return Success;
        }

        public int DeleteResourceDetailsRepository(int resourceid)
        {
            int Success = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_ResourceDetailsDelete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ResourceId", resourceid);

                    conn.Open();
                    Success = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return Success;
        }
    }
}
