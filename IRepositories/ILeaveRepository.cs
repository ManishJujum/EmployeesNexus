using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface ILeaveRepository
    {
        public List<LeaveMaster> GetAllLeavesRepository();

        public LeaveMaster GetAttendenceDetailsbYEmployeeIdAndCurrentDateRepository(int resourceid);

        public List<LeaveMaster> GetAllLeavesRepository(int resourceid);

        public List<Attendence> GetAttendenceListByResourceIdRepository(int resourceId);

        public LeaveMaster GetAllLeaveDetailsByResourceIdAndLeaveIdRepository(int resourceid, int LeaveId);

        public List<EmployeeProfileDetails> GetAllEmployeesRepository();

        public void InsertEmployeeLeaveRepository(int employeeId, int leaveTypeId, DateTime leaveStartDate, DateTime leaveEndDate, string leaveReason, string leaveStatus,
        DateTime appliedDate, int approvedBy, string remarks, byte[] attachment = null);
    }
}
