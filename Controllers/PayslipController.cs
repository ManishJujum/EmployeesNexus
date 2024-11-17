using Microsoft.AspNetCore.Mvc;

namespace EmployeeNexus.Controllers
{
    public class PayslipController : Controller
    {
        public IActionResult EmployeePayslip()
        {
            return View();
        }


        public IActionResult DownloadPayslip()
        {
            // Path to the PDF file in the project folder or server
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", "SalarySleep.pdf");

            if (System.IO.File.Exists(filePath))
            {
                // Read the PDF file content
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var contentType = "application/pdf";
                var fileName = "SalarySleep.pdf";

                // Return the file as a download
                return File(fileBytes, contentType, fileName);
            }
            else
            {
                // Handle the case when the file does not exist
                return NotFound("The requested file was not found on the server.");
            }
        }
    }
}
