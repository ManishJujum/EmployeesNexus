using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.Design;
using Microsoft.Extensions.Hosting;
using BusinessModel;
using BusinessLogicLayer.IServices;

namespace EmployeeNexus.Controllers
{
    public class ManageResourceDetails : Controller
    {
        private readonly IManageResourceDetailsService _IManageResourceDetailsService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ManageResourceDetails(IManageResourceDetailsService iManageResourceDetailsService, IWebHostEnvironment hostEnvironment)
        {
            _IManageResourceDetailsService = iManageResourceDetailsService;
            _hostEnvironment = hostEnvironment;
        }


        [HttpGet]
        public IActionResult ManageResource(string searchText, int? organizationId)
        {
            List<EmployeeProfileDetails> resourcelist;
            resourcelist = _IManageResourceDetailsService.GetResourceDetailsService();
            ViewBag.Department = _IManageResourceDetailsService.GetDepartmentMasterlistService();
            ViewBag.Roles = _IManageResourceDetailsService.GetRolesService();
            return View(resourcelist);
        }

        [HttpGet]
        public JsonResult FilterManageResource(string searchText, int? organizationId)
        {
            List<EmployeeProfileDetails> resourcelist;
            if (!string.IsNullOrEmpty(searchText) || organizationId > 0)
            {
                resourcelist = _IManageResourceDetailsService.GetFilteredResourceDetailsService(searchText, organizationId);
            }
            else
            {
                resourcelist = _IManageResourceDetailsService.GetResourceDetailsService();
            }
            return Json(resourcelist);
        }

        [HttpGet]
        public IActionResult AddResourceDetails()
        {
            try
            {
                ViewBag.Department = _IManageResourceDetailsService.GetDepartmentMasterlistService();
                ViewBag.Roles = _IManageResourceDetailsService.GetRolesService();
                ViewBag.ReportingManagers = _IManageResourceDetailsService.GetAllReportingManagersService();

                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddResourceDetails(EmployeeProfileDetails _ResourceDetails, IFormFile ProfileImage)
        {
            try
            {
                // Handle image upload if an image is provided
                if (ProfileImage != null && ProfileImage.Length > 0)
                {
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");

                    // Create directory if it does not exist
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    // Create a unique file name
                    string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(ProfileImage.FileName)}";
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    // Save the image to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfileImage.CopyTo(fileStream);
                    }

                    // Set the image path in ResourceDetails
                    _ResourceDetails.ImagePath = uniqueFileName;
                }

                // Call your layer methods to save resource details
                int success = _IManageResourceDetailsService.AddResourceDetailsService(_ResourceDetails);
                int isSuccess = _IManageResourceDetailsService.AddResourceCredentialsService(success, _ResourceDetails.LoginName, _ResourceDetails.Password);

                // Return success message
                return Json(new { success = true, message = "Resource added successfully" });
            }
            catch (Exception ex)
            {
                // Return error message
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpGet]
        public IActionResult UpdateResourceDetails(int Id)
        {
            ViewBag.Department = _IManageResourceDetailsService.GetDepartmentMasterlistService();
            ViewBag.Roles = _IManageResourceDetailsService.GetRolesService();
            ViewBag.ReportingManagers = _IManageResourceDetailsService.GetAllReportingManagersService();

            EmployeeProfileDetails resource = _IManageResourceDetailsService.GetResourceDetailsByResourceIdService(Id);
            //ViewBag.ProfileImagePath = resource.ImagePath;
            if (resource == null)
            {
                return RedirectToAction("ManageResource");
            }
            else
            {
                return View(resource);
            }
        }

        [HttpPost]
        public IActionResult UpdateResourceDetails(EmployeeProfileDetails _ResourceDetails, int Id, IFormFile ProfileImage)
        {
            try
            {
                // Check if a new profile image is uploaded
                if (ProfileImage != null && ProfileImage.Length > 0)
                {
                    // Define the upload folder path
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");

                    // Create directory if it does not exist
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    // Create a unique file name
                    string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(ProfileImage.FileName)}";
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    // Save the image to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfileImage.CopyTo(fileStream);
                    }

                    // Set the image path in ResourceDetails
                    _ResourceDetails.ImagePath = uniqueFileName; // Set the new image path
                }

                // Update resource details in the database
                int res = _IManageResourceDetailsService.UpdateResourceDetailsService(_ResourceDetails, Id);

                int ress = _IManageResourceDetailsService.UpdateManageUserDetailsService(_ResourceDetails, Id);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("ManageResource");
        }


        [HttpPost]
        public IActionResult DeleteResourceDetails(int resourceid)
        {
            try
            {
                int result = _IManageResourceDetailsService.DeleteResourceDetailsService(resourceid);

                if (result > 0)
                {
                    return Json(new { success = true, message = "Resource deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Deletion failed. Resource not found or could not be deleted." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
