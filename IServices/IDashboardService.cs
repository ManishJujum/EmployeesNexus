using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IDashboardService
    {
        public List<EmployeeProfileDetails> GetAllEmployeesService();
        public List<EmployeeProfileDetails> GetAllReportingManagersService();
    }
}
