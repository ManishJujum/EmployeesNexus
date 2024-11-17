using BusinessModel;
using DataAccessLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repositories
{
    public class AttendenceRepository : IAttendenceRepository
    {
        string? _connectionString = string.Empty;
        public AttendenceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
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

        public List<Attendence> GetAttendenceListByResourceIdRepository(int resourceId)
        {
            List<Attendence> attendenceList = new List<Attendence>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetSumOfAttendenceByResourceId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ResourceId", resourceId));

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Attendence attendence = new Attendence
                            {
                                Total = Convert.ToInt32(reader["Total"]),
                                Present = Convert.ToInt32(reader["Present"])
                            };
                            attendenceList.Add(attendence);
                        }
                    }
                }
            }

            return attendenceList;
        }

        public Attendence GetAllAttendenceHoursByEmployeeIdRepository(int EmployeeId)
        {
            Attendence attendence = new Attendence();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllAttendenceHours", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@EmployeeId", EmployeeId));

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            attendence = new Attendence
                            {
                                TotalWorkingHours = Convert.ToDecimal(reader["TotalWorkingHours"]),
                                OvertimeHours = Convert.ToDecimal(reader["OvertimeHours"]),
                                AbsentHours = Convert.ToDecimal(reader["AbsentHours"]),
                                ProductiveHours = Convert.ToDecimal(reader["ProductiveHours"]),
                                LeaveHours = Convert.ToDecimal(reader["LeaveHours"]),
                                LateArrivalHours = Convert.ToDecimal(reader["LateArrivalHours"]),
                            };
                        }
                    }
                }
            }

            return attendence;
        }

        public Attendence GetAttendenceByResourceIdRepository(int id)
        {
            Attendence result = new Attendence();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAttendenceByResourceId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ID", id));

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new Attendence
                            {
                                AttendenceStatus = reader["AttendenceStatus"].ToString(),
                                ResourceName = reader["ResourceName"].ToString(),
                            };
                        }
                    }
                }
            }

            return result;
        }

        public Attendence GetAttendenceDetailsbYEmployeeIdAndCurrentDateRepository(int resourceid, DateTime date)
        {
            Attendence result = new Attendence();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetEmployeeAttendence", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Adding parameters for the stored procedure
                        command.Parameters.Add(new SqlParameter("@resourceid", resourceid));
                        command.Parameters.Add(new SqlParameter("@date", date.Date));

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Read data from the stored procedure
                        if (reader.Read())
                        {
                            result = new Attendence
                            {
                                TotalWorkingHours = Convert.ToDecimal(reader["totalworkinghours"])
                            };
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public Attendence GetAttendenceStatusByDateAndResourceIdRepository(int resourceid, DateTime date)
        {
            Attendence result = new Attendence();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("getAttendenceStatusByDateAndResourceId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@resourceid", resourceid));
                    command.Parameters.Add(new SqlParameter("@date", date));

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new Attendence
                            {
                                Present = Convert.ToInt32(reader["Present"]),
                            };
                        }
                    }
                    connection.Close();
                }
            }

            return result;
        }
    }
}
