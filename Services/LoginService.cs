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
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _ILoginRepository;
        public LoginService(ILoginRepository iLoginRepository) 
        {
            _ILoginRepository = iLoginRepository;
        }

        public EmployeeProfileDetails LoginAuthenticationService(string LoginName, string Password)
        {
            return _ILoginRepository.LoginAuthenticationRepository(LoginName, Password);
        }
    }
}
