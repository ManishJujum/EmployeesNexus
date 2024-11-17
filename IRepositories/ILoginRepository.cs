using BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface ILoginRepository
    {
        public EmployeeProfileDetails LoginAuthenticationRepository(string LoginName, string Password);
    }
}
