using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using BusinessLogicLayer.IServices;
using BusinessModel;
//using Microsoft.AspNetCore.Mvc;


namespace EmployeeNexus.Controllers
{
    public class LeaveController : Controller
    {
        public ILeaveService _ILeaveService; EmployeeProfileDetails loginUserdata = new EmployeeProfileDetails();
        public LeaveController(ILeaveService leaveService)
        {
            _ILeaveService = leaveService;
        }

        public IActionResult EmployeeLeave(int? year = null, int? month = null)
        {

            ViewBag.LeaveTypes = _ILeaveService.GetAllLeavesService();

            var credentials = HttpContext.Session.GetString("UserDetails");

            if (credentials != null)
            {
                loginUserdata = JsonConvert.DeserializeObject<EmployeeProfileDetails>(credentials);
            }
            LeaveMaster leaveMasters = _ILeaveService.GetAttendenceDetailsbYEmployeeIdAndCurrentDateService(loginUserdata.Id);

            ViewBag.Pending = leaveMasters.Pending;
            ViewBag.Approved = leaveMasters.Approved;
            ViewBag.Rejected = leaveMasters.Rejected;
            ViewBag.Available = leaveMasters.Available;


            //get all leave details
            List<LeaveMaster> leaveMasterslist = _ILeaveService.GetAllLeavesService(loginUserdata.Id);
            ViewBag.leaveMasterslist = leaveMasterslist;


            // Get the current date if no parameters are provided
            DateTime currentDate = DateTime.Now;

            // Use the provided year and month, or default to the current month
            int targetYear = year ?? currentDate.Year;
            int targetMonth = month ?? currentDate.Month;

            // List to hold all dates for the target month
            List<DateTime> currentMonthDates = new List<DateTime>();

            // Start from the first day of the target month
            DateTime startDate = new DateTime(targetYear, targetMonth, 1);

            // Get the last day of the target month
            DateTime endDate = startDate.AddMonths(1).AddDays(-1); // This gives you the last day of the month

            // Loop through each date from the start date to the end date
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                currentMonthDates.Add(date); // Add the date to the list
            }

            // Pass the current month dates to the view
            ViewBag.AllDates = currentMonthDates;

            // Pass the current year and month to the view
            ViewBag.CurrentYear = targetYear;
            ViewBag.CurrentMonth = targetMonth;

            string monthname = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(targetMonth);
            ViewBag.monthname = monthname;

            ViewBag.CurrentDay = DateTime.Now.Day;


            var attendanceData = new List<AttendanceRecord>
            {
              new AttendanceRecord { EmployeeName = "1st Week", PresentDays = 4, AbsentDays = 2, SickLeaves = 1 },
              new AttendanceRecord { EmployeeName = "2nd Week", PresentDays = 5, AbsentDays = 2, SickLeaves = 1 },
              new AttendanceRecord { EmployeeName = "3rd Week", PresentDays = 6, AbsentDays = 4, SickLeaves = 0 },
              new AttendanceRecord { EmployeeName = "4th Week", PresentDays = 7, AbsentDays = 0, SickLeaves = 1 },
            };

            List<EmployeeProfileDetails> resources = new List<EmployeeProfileDetails>();
            resources = _ILeaveService.GetAllEmployeesService();
            ViewBag.ResourceList = resources.Where(c => c.Id == loginUserdata.Id);


            DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            int Present = 0;
            int Absent = 0;
            List<Attendence> PresentattendenceList = _ILeaveService.GetAttendenceListByResourceIdService(loginUserdata.Id);

            Present = PresentattendenceList.Where(c => c.Present == 1).Select(c => c.Total).FirstOrDefault();
            Absent = PresentattendenceList.Where(c => c.Present == 0).Select(c => c.Total).FirstOrDefault();
            ViewBag.Present = Present;
            ViewBag.absent = Absent;

            return View();
        }

        [HttpPost]
        public IActionResult Create(int employeeId, int leaveTypeId, DateTime leaveStartDate, DateTime leaveEndDate, string leaveReason, string leaveStatus,
    DateTime appliedDate, int approvedBy, string remarks, IFormFile attachment = null)
        {
            byte[] attachmentBytes = null;

            // Handle file attachment
            if (attachment != null && attachment.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    attachment.CopyTo(memoryStream);
                    attachmentBytes = memoryStream.ToArray(); // Convert to byte array
                }
            }

            var credentials = HttpContext.Session.GetString("UserDetails");
            EmployeeProfileDetails loginUserdata = null;

            if (credentials != null)
            {
                loginUserdata = JsonConvert.DeserializeObject<EmployeeProfileDetails>(credentials);
            }

            // Call the method to insert the leave record
            _ILeaveService.InsertEmployeeLeaveService(
                loginUserdata.Id,
                leaveTypeId,
                leaveStartDate,
                leaveEndDate,
                leaveReason,
                "Pending",
                DateTime.Now, // Use the appliedDate parameter
                loginUserdata.ReportingManager,
                remarks, // Pass remarks instead of leaveReason
                attachmentBytes // Now this is a byte[]
            );
            return RedirectToAction("EmployeeLeave");
        }

        public IActionResult DownloadFile(int id)
        {
            var credentials = HttpContext.Session.GetString("UserDetails");

            if (credentials != null)
            {
                loginUserdata = JsonConvert.DeserializeObject<EmployeeProfileDetails>(credentials);
            }

            var leaveRecord = _ILeaveService.GetAllLeaveDetailsByResourceIdAndLeaveIdService(loginUserdata.Id, id);

            if (leaveRecord?.AttachmentBytes != null)
            {
                var fileContent = leaveRecord.AttachmentBytes;
                var contentType = "application/octet-stream"; // Set appropriate MIME type if known
                var fileName = "Attachment_" + id + ".pdf"; // Adjust file name and extension as necessary

                return File(fileContent, contentType, fileName);
            }

            return RedirectToAction("EmployeeLeave");
        }
    }
}
