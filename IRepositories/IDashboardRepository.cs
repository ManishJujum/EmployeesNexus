using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface IDashboardRepository
    {
        public List<EmployeeProfileDetails> GetAllEmployeesRepository();
        public List<EmployeeProfileDetails> GetAllReportingManagersRepository();
    }
}
