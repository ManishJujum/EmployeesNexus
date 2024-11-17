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
    public class LeaveRepository : ILeaveRepository
    {
        string? _connectionString = string.Empty;
        public LeaveRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<LeaveMaster> GetAllLeavesRepository()
        {
            List<LeaveMaster> leaves = new List<LeaveMaster>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getAllLeaves", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    SqlDataReader read = cmd.ExecuteReader();

                    while (read.Read())
                    {
                        LeaveMaster leaveMaster = new LeaveMaster();
                        leaveMaster.LeaveID = Convert.ToInt32(read["LeaveID"]);
                        leaveMaster.LeaveType = Convert.ToString(read["LeaveType"]);
                        leaves.Add(leaveMaster);
                    }
                    con.Close();
                }
            }
            return leaves;
        }

        public LeaveMaster GetAttendenceDetailsbYEmployeeIdAndCurrentDateRepository(int resourceid)
        {
            LeaveMaster result = new LeaveMaster();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("getAllLeaveCountByResourceId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@ResourceId", resourceid));

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            result = new LeaveMaster
                            {
                                Pending = Convert.ToInt32(reader["Pending"]),
                                Approved = Convert.ToInt32(reader["Approved"]),
                                Rejected = Convert.ToInt32(reader["Rejected"]),
                                Available = Convert.ToInt32(reader["Available"])
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

        public List<LeaveMaster> GetAllLeavesRepository(int resourceid)
        {
            List<LeaveMaster> leaves = new List<LeaveMaster>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getAllLeaveDetailsByResourceId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@ResourceId", resourceid));

                    con.Open();

                    SqlDataReader read = cmd.ExecuteReader();

                    while (read.Read())
                    {
                        LeaveMaster leaveMaster = new LeaveMaster();
                        leaveMaster.LeaveID = Convert.ToInt32(read["LeaveID"]);
                        leaveMaster.LeaveType = Convert.ToString(read["LeaveType"]);
                        leaveMaster.LeaveReason = Convert.ToString(read["LeaveReason"]);
                        leaveMaster.Remarks = Convert.ToString(read["Remarks"]);
                        leaveMaster.LeaveStartDate = Convert.ToDateTime(read["LeaveStartDate"]);
                        leaveMaster.LeaveEndDate = Convert.ToDateTime(read["LeaveEndDate"]);
                        leaveMaster.LeaveStatus = Convert.ToString(read["LeaveStatus"]);
                        leaveMaster.AppliedDate = Convert.ToDateTime(read["AppliedDate"]);
                        leaveMaster.Approver = Convert.ToString(read["Approver"]);
                        leaveMaster.AttachmentBytes = read["Attachment"] as byte[];
                        leaves.Add(leaveMaster);
                    }
                    con.Close();
                }
            }
            return leaves;
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

        public LeaveMaster GetAllLeaveDetailsByResourceIdAndLeaveIdRepository(int resourceid, int LeaveId)
        {
            LeaveMaster result = new LeaveMaster();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("getAllLeaveDetailsByResourceIdAndLeaveId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@ResourceId", resourceid));
                        command.Parameters.Add(new SqlParameter("@LeaveId", LeaveId));

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            result = new LeaveMaster
                            {
                                //LeaveID = Convert.ToInt32(reader["Pending"]),
                                AttachmentBytes = reader["Attachment"] as byte[]
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

        public void InsertEmployeeLeaveRepository(int employeeId, int leaveTypeId, DateTime leaveStartDate, DateTime leaveEndDate, string leaveReason, string leaveStatus,
        DateTime appliedDate, int approvedBy, string remarks, byte[] attachment = null) // Change to byte[]
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertEmployeeLeave", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@LeaveStartDate", leaveStartDate);
                    cmd.Parameters.AddWithValue("@LeaveEndDate", leaveEndDate);
                    cmd.Parameters.AddWithValue("@LeaveTypeId", (object)leaveTypeId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LeaveReason", (object)leaveReason ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LeaveStatus", leaveStatus);
                    cmd.Parameters.AddWithValue("@AppliedDate", (object)appliedDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ApprovedBy", (object)approvedBy ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Remarks", (object)remarks ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Attachment", (object)attachment ?? DBNull.Value);

                    // Open the connection
                    conn.Open();

                    // Execute the command
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
