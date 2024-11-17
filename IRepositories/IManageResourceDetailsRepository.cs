using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface IManageResourceDetailsRepository
    {
        public List<EmployeeProfileDetails> GetResourceDetailsRepository();

        public List<DepartmentMaster> GetDepartmentMasterlistRepository();

        public List<EmployeeProfileDetails> GetRolesRepository();

        public List<EmployeeProfileDetails> GetFilteredResourceDetailsRepository(string searchText, int? organizationId);

        public List<EmployeeProfileDetails> GetAllReportingManagersRepository();

        public int AddResourceDetailsRepository(EmployeeProfileDetails resourceDetails);

        public int AddResourceCredentialsRepository(int resourceId, string username, string password);

        public EmployeeProfileDetails GetResourceDetailsByResourceIdRepository(int id);

        public int UpdateResourceDetailsRepository(EmployeeProfileDetails resourceDetails, int resourceid);

        public int UpdateManageUserDetailsRepository(EmployeeProfileDetails resourceDetails, int resourceid);

        public int DeleteResourceDetailsRepository(int resourceid);
    }
}
