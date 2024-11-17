using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IAttendanceService
    {
        public List<EmployeeProfileDetails> GetAllEmployeesService();
        public List<Attendence> GetAttendenceListByResourceIdService(int resourceId);
        public Attendence GetAllAttendenceHoursByEmployeeIdService(int EmployeeId);
        public Attendence GetAttendenceByResourceIdService(int id);
        public Attendence GetAttendenceDetailsbYEmployeeIdAndCurrentDateService(int resourceid, DateTime date);
        public Attendence GetAttendenceStatusByDateAndResourceIdService(int resourceid, DateTime date);
        public List<DateTime> GetDatesForMonth(int year, int month);
        public (int Present, int Absent) GetAttendenceStatusForMonth(int resourceId);
        public List<EmployeeProfileDetails> GetEmployeeProfileDetails(int employeeId);
    }
}
