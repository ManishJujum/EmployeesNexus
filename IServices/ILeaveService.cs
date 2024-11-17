using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface ILeaveService
    {
        public List<LeaveMaster> GetAllLeavesService();

        public LeaveMaster GetAttendenceDetailsbYEmployeeIdAndCurrentDateService(int resourceid);

        public List<LeaveMaster> GetAllLeavesService(int resourceid);

        public List<Attendence> GetAttendenceListByResourceIdService(int resourceId);

        public LeaveMaster GetAllLeaveDetailsByResourceIdAndLeaveIdService(int resourceid, int LeaveId);

        public List<EmployeeProfileDetails> GetAllEmployeesService();

        public void InsertEmployeeLeaveService(int employeeId, int leaveTypeId, DateTime leaveStartDate, DateTime leaveEndDate, string leaveReason, string leaveStatus,
        DateTime appliedDate, int approvedBy, string remarks, byte[] attachment = null);
    }
}
