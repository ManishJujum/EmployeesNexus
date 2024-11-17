using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface IAttendenceRepository
    {
        public List<EmployeeProfileDetails> GetAllEmployeesRepository();
        public List<Attendence> GetAttendenceListByResourceIdRepository(int resourceId);
        public Attendence GetAllAttendenceHoursByEmployeeIdRepository(int EmployeeId);
        public Attendence GetAttendenceByResourceIdRepository(int id);
        public Attendence GetAttendenceDetailsbYEmployeeIdAndCurrentDateRepository(int resourceid, DateTime date);
        public Attendence GetAttendenceStatusByDateAndResourceIdRepository(int resourceid, DateTime date);
    }
}
