using BusinessLogicLayer.IServices;
using BusinessModel;
using DataAccessLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _ILeaveRepository;
        public LeaveService(ILeaveRepository iLeaveRepository) 
        {
            _ILeaveRepository = iLeaveRepository;
        }

        public List<LeaveMaster> GetAllLeavesService()
        {
            return _ILeaveRepository.GetAllLeavesRepository();
        }

        public LeaveMaster GetAttendenceDetailsbYEmployeeIdAndCurrentDateService(int resourceid)
        {
            return _ILeaveRepository.GetAttendenceDetailsbYEmployeeIdAndCurrentDateRepository(resourceid);
        }

        public List<LeaveMaster> GetAllLeavesService(int resourceid)
        {
            return _ILeaveRepository.GetAllLeavesRepository(resourceid);
        }

        public List<Attendence> GetAttendenceListByResourceIdService(int resourceId)
        {
            return _ILeaveRepository.GetAttendenceListByResourceIdRepository(resourceId);
        }

        public LeaveMaster GetAllLeaveDetailsByResourceIdAndLeaveIdService(int resourceid, int LeaveId)
        {
            return _ILeaveRepository.GetAllLeaveDetailsByResourceIdAndLeaveIdRepository(resourceid, LeaveId);
        }

        public List<EmployeeProfileDetails> GetAllEmployeesService()
        {
            return _ILeaveRepository.GetAllEmployeesRepository();
        }

        public void InsertEmployeeLeaveService(int employeeId, int leaveTypeId, DateTime leaveStartDate, DateTime leaveEndDate, string leaveReason, string leaveStatus,
        DateTime appliedDate, int approvedBy, string remarks, byte[] attachment = null)
        {
            _ILeaveRepository.InsertEmployeeLeaveRepository(employeeId, leaveTypeId, leaveStartDate, leaveEndDate, leaveReason, leaveStatus, appliedDate, approvedBy, remarks, attachment);
        }
    }
}
