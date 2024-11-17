using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface ILoginService
    {
        public EmployeeProfileDetails LoginAuthenticationService(string LoginName, string Password);
    }
}
