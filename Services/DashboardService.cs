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
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _IDashboardRepository;
        public DashboardService(IDashboardRepository _dashboardRepository)
        {
            _IDashboardRepository = _dashboardRepository;
        }

        public List<EmployeeProfileDetails> GetAllEmployeesService()
        {
            return _IDashboardRepository.GetAllEmployeesRepository();
        }
        public List<EmployeeProfileDetails> GetAllReportingManagersService()
        {
            return _IDashboardRepository.GetAllReportingManagersRepository();
        }
    }
}
