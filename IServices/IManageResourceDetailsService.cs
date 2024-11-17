using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IManageResourceDetailsService
    {
        public List<EmployeeProfileDetails> GetResourceDetailsService();

        public List<DepartmentMaster> GetDepartmentMasterlistService();

        public List<EmployeeProfileDetails> GetRolesService();

        public List<EmployeeProfileDetails> GetFilteredResourceDetailsService(string searchText, int? organizationId);

        public List<EmployeeProfileDetails> GetAllReportingManagersService();

        public int AddResourceDetailsService(EmployeeProfileDetails resourceDetails);

        public int AddResourceCredentialsService(int resourceId, string username, string password);

        public EmployeeProfileDetails GetResourceDetailsByResourceIdService(int id);

        public int UpdateResourceDetailsService(EmployeeProfileDetails resourceDetails, int resourceid);

        public int UpdateManageUserDetailsService(EmployeeProfileDetails resourceDetails, int resourceid);

        public int DeleteResourceDetailsService(int resourceid);
    }
}
