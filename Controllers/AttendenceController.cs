using BusinessModel;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BusinessLogicLayer.IServices;

namespace EmployeeNexus.Controllers
{
    public class AttendenceController : Controller
    {
        private readonly IAttendanceService _IAttendanceService;

        EmployeeProfileDetails loginUserdata = new EmployeeProfileDetails();
        public AttendenceController(IAttendanceService iAttendanceService)
        {
            _IAttendanceService = iAttendanceService;
        }

        //public IActionResult EmployeeAttendence(int? year = null, int? month = null)
        //{
        //    // Get the current date if no parameters are provided
        //    DateTime currentDate = DateTime.Now;

        //    // Use the provided year and month, or default to the current month
        //    int targetYear = year ?? currentDate.Year;
        //    int targetMonth = month ?? currentDate.Month;

        //    // List to hold all dates for the target month
        //    List<DateTime> currentMonthDates = new List<DateTime>();

        //    // Start from the first day of the target month
        //    DateTime startDate = new DateTime(targetYear, targetMonth, 1);

        //    // Get the last day of the target month
        //    DateTime endDate = startDate.AddMonths(1).AddDays(-1); // This gives you the last day of the month

        //    // Loop through each date from the start date to the end date
        //    for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
        //    {
        //        currentMonthDates.Add(date); // Add the date to the list
        //    }

        //    // Pass the current month dates to the view
        //    ViewBag.AllDates = currentMonthDates;

        //    // Pass the current year and month to the view
        //    ViewBag.CurrentYear = targetYear;
        //    ViewBag.CurrentMonth = targetMonth;

        //    string monthname = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(targetMonth);
        //    ViewBag.monthname = monthname;

        //    ViewBag.CurrentDay = DateTime.Now.Day;


        //    var attendanceData = new List<AttendanceRecord>
        //    {
        //      new AttendanceRecord { EmployeeName = "1st Week", PresentDays = 4, AbsentDays = 2, SickLeaves = 1 },
        //      new AttendanceRecord { EmployeeName = "2nd Week", PresentDays = 5, AbsentDays = 2, SickLeaves = 1 },
        //      new AttendanceRecord { EmployeeName = "3rd Week", PresentDays = 6, AbsentDays = 4, SickLeaves = 0 },
        //      new AttendanceRecord { EmployeeName = "4th Week", PresentDays = 7, AbsentDays = 0, SickLeaves = 1 },
        //    };


        //    var credentials = HttpContext.Session.GetString("UserDetails");

        //    if (credentials != null)
        //    {
        //        loginUserdata = JsonConvert.DeserializeObject<EmployeeProfileDetails>(credentials);
        //    }

        //    //get and display total attendence hours 
        //    Attendence attendence = TotalAttendenceHours(loginUserdata.Id);
        //    ViewBag.TotalWorkingHours = attendence.TotalWorkingHours;
        //    ViewBag.OvertimeHours = attendence.OvertimeHours;
        //    ViewBag.AbsentHours = attendence.AbsentHours;
        //    ViewBag.ProductiveHours = attendence.ProductiveHours;
        //    ViewBag.LeaveHours = attendence.LeaveHours;
        //    ViewBag.LateArrivalHours = attendence.LateArrivalHours;

        //    List<EmployeeProfileDetails> resources = new List<EmployeeProfileDetails>();
        //    resources = _businessLayer.GetAllEmployees();
        //    ViewBag.ResourceList = resources.Where(c => c.Id == loginUserdata.Id);


        //    DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //    DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        //    int Present = 0;
        //    int Absent = 0;
        //    List<Attendence> PresentattendenceList =  _businessLayer.GetAttendenceListByResourceId(loginUserdata.Id);

        //    Present = PresentattendenceList.Where(c => c.Present == 1).Select(c => c.Total).FirstOrDefault();
        //    Absent = PresentattendenceList.Where(c => c.Present == 0).Select(c => c.Total).FirstOrDefault();
        //    ViewBag.Present = Present;
        //    ViewBag.absent = Absent;

        //    return View(attendanceData);
        //}


        //getting monthly total hours of employee by employee id 


        public IActionResult EmployeeAttendence(int? year = null, int? month = null)
        {
            // Get the current date if no parameters are provided
            DateTime currentDate = DateTime.Now;
            int targetYear = year ?? currentDate.Year;
            int targetMonth = month ?? currentDate.Month;

            // Get the dates for the target month
            var currentMonthDates = _IAttendanceService.GetDatesForMonth(targetYear, targetMonth);
            ViewBag.AllDates = currentMonthDates;

            // Set the current year, month, and month name in ViewBag
            ViewBag.CurrentYear = targetYear;
            ViewBag.CurrentMonth = targetMonth;
            ViewBag.monthname = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(targetMonth);
            ViewBag.CurrentDay = currentDate.Day;

            // Fetch attendance data for the employee
            var attendanceData = new List<AttendanceRecord>
            {
                new AttendanceRecord { EmployeeName = "1st Week", PresentDays = 4, AbsentDays = 2, SickLeaves = 1 },
                new AttendanceRecord { EmployeeName = "2nd Week", PresentDays = 5, AbsentDays = 2, SickLeaves = 1 },
                new AttendanceRecord { EmployeeName = "3rd Week", PresentDays = 6, AbsentDays = 4, SickLeaves = 0 },
                new AttendanceRecord { EmployeeName = "4th Week", PresentDays = 7, AbsentDays = 0, SickLeaves = 1 },
            };

            var credentials = HttpContext.Session.GetString("UserDetails");
            EmployeeProfileDetails loginUserdata = null;

            if (credentials != null)
            {
                loginUserdata = JsonConvert.DeserializeObject<EmployeeProfileDetails>(credentials);
            }

            // Get total attendance hours
            var attendence = _IAttendanceService.GetAllAttendenceHoursByEmployeeIdService(loginUserdata.Id);
            ViewBag.TotalWorkingHours = attendence.TotalWorkingHours;
            ViewBag.OvertimeHours = attendence.OvertimeHours;
            ViewBag.AbsentHours = attendence.AbsentHours;
            ViewBag.ProductiveHours = attendence.ProductiveHours;
            ViewBag.LeaveHours = attendence.LeaveHours;
            ViewBag.LateArrivalHours = attendence.LateArrivalHours;

            // Get employee details for the current user
            var resources = _IAttendanceService.GetEmployeeProfileDetails(loginUserdata.Id);
            ViewBag.ResourceList = resources;

            // Get attendance status for the current month
            var (Present, Absent) = _IAttendanceService.GetAttendenceStatusForMonth(loginUserdata.Id);
            ViewBag.Present = Present;
            ViewBag.Absent = Absent;

            return View(attendanceData);
        }

        public Attendence TotalAttendenceHours(int ResourceId)
        {
            Attendence attendence = new Attendence();
            try
            {
                attendence = _IAttendanceService.GetAllAttendenceHoursByEmployeeIdService(ResourceId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return attendence;
        }

        public JsonResult GetAttendenceStatusByRsource()
        {
            Attendence CurrentAttendenceStatus = new Attendence();
            try
            {
                var credentials = HttpContext.Session.GetString("UserDetails");

                if (credentials != null)
                {
                    loginUserdata = JsonConvert.DeserializeObject<EmployeeProfileDetails>(credentials);
                }
                CurrentAttendenceStatus = _IAttendanceService.GetAttendenceByResourceIdService(loginUserdata.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Json(CurrentAttendenceStatus);
        }

        public JsonResult GetAttendenceStatusByResourceIdCurrentDate(DateTime date)
        {
            var credentials = HttpContext.Session.GetString("UserDetails");

            if (credentials != null)
            {
                loginUserdata = JsonConvert.DeserializeObject<EmployeeProfileDetails>(credentials);
            }
            Attendence attendenceHours = _IAttendanceService.GetAttendenceDetailsbYEmployeeIdAndCurrentDateService(loginUserdata.Id, date);

            Attendence attendenceStatus = _IAttendanceService.GetAttendenceStatusByDateAndResourceIdService(loginUserdata.Id, date);

            Attendence attendence = new Attendence
            {
                TotalWorkingHours = attendenceHours.TotalWorkingHours,
                currentdate = attendenceHours.currentdate,
                Present = attendenceStatus.Present,
            };

            return Json(attendence);
        }
    }
}
