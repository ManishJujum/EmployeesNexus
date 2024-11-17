using BusinessLogicLayer.IServices;
using BusinessModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeNexus.Controllers
{
    public class DashboardController : Controller
    {
        private IDashboardService _DashboardService;
        public DashboardController(IDashboardService dashboardService) 
        { 
            _DashboardService = dashboardService; 
        }

        public IActionResult Index()
        {
            var credentials = HttpContext.Session.GetString("UserDetails");    

            if(credentials != null)
            {
                EmployeeProfileDetails? loginUserdata = JsonConvert.DeserializeObject<EmployeeProfileDetails>(credentials);
            }

            //for dropdownlist
            List<EmployeeProfileDetails> resources = new List<EmployeeProfileDetails>();
            resources = _DashboardService.GetAllEmployeesService();
            ViewBag.ResourceList = resources;
            ViewBag.ReportingManagers = _DashboardService.GetAllReportingManagersService();

            return View();
        }

    }
}
