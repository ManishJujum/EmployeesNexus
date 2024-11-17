using BusinessLogicLayer.IServices;
using BusinessModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.IRepositories;

namespace BusinessLogicLayer.Services
{
    public class ManageResourceDetailsService : IManageResourceDetailsService
    {
        private readonly IManageResourceDetailsRepository _IManageResourceDetailsRepository;
        public ManageResourceDetailsService(IManageResourceDetailsRepository imManageResourceDetailsRepository) 
        {
            _IManageResourceDetailsRepository = imManageResourceDetailsRepository;
        }

        public List<EmployeeProfileDetails> GetResourceDetailsService()
        {
            return _IManageResourceDetailsRepository.GetResourceDetailsRepository();
        }

        public List<DepartmentMaster> GetDepartmentMasterlistService()
        {
            return _IManageResourceDetailsRepository.GetDepartmentMasterlistRepository();
        }

        public List<EmployeeProfileDetails> GetRolesService()
        {
            return _IManageResourceDetailsRepository.GetRolesRepository();
        }

        public List<EmployeeProfileDetails> GetFilteredResourceDetailsService(string searchText, int? organizationId)
        {
            return _IManageResourceDetailsRepository.GetFilteredResourceDetailsRepository(searchText, organizationId);
        }

        public List<EmployeeProfileDetails> GetAllReportingManagersService()
        {
            return _IManageResourceDetailsRepository.GetAllReportingManagersRepository();
        }

        public int AddResourceDetailsService(EmployeeProfileDetails resourceDetails)
        {
            return _IManageResourceDetailsRepository.AddResourceDetailsRepository(resourceDetails);
        }

        public int AddResourceCredentialsService(int resourceId, string username, string password)
        {
            return _IManageResourceDetailsRepository.AddResourceCredentialsRepository(resourceId, username, password);
        }

        public EmployeeProfileDetails GetResourceDetailsByResourceIdService(int id)
        {
            var existingdetails = _IManageResourceDetailsRepository.GetResourceDetailsByResourceIdRepository(id);
            return existingdetails;
        }

        public int UpdateResourceDetailsService(EmployeeProfileDetails resourceDetails, int resourceid)
        {
            return _IManageResourceDetailsRepository.UpdateResourceDetailsRepository(resourceDetails, resourceid);
        }

        public int UpdateManageUserDetailsService(EmployeeProfileDetails resourceDetails, int resourceid)
        {
            return _IManageResourceDetailsRepository.UpdateManageUserDetailsRepository(resourceDetails, resourceid);
        }

        public int DeleteResourceDetailsService(int resourceid)
        {
            return _IManageResourceDetailsRepository.DeleteResourceDetailsRepository(resourceid);
        }
    }
}
