using Microsoft.AspNetCore.Http;

namespace BusinessModel
{
    public class EmployeeProfileDetails
    {
        public int Id { get; set; }
        public string? ResourceName { get; set; }
        public string? RoleName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public int UserRole { get; set; }
        public int OrgId { get; set; }
        public long Pincode { get; set; }
        public string? City { get; set; }
        public string? OrgName { get; set; }
        public string? Description { get; set; }
        public int RoleId { get; set; }
        public string? state { get; set; }
        public int DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public int ReportingManager { get; set; }
        public string? ReportingManagerName { get; set; }
        public string? LoginName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? JoiningDate { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? ImagePath { get; set; }
        public string? EmployeeCode { get; set; }
        public string? CurrentJobStatus { get; set; }
        public string? EmployeeStatus { get; set; }
    }

    public class LoginCredentials
    {
        public string? LoginName { get; set; }
        public string? Password { get; set; }
        public string? RoleName { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSpoc { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public int OrgId { get; set; }
        public string? OrgName { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
    }

    public class ProjectCodeMaseter
    {
        public int ProjectCodeId { get; set; }
        public int CustomerId { get; set; }
        public long TotalCost { get; set; }
        public string? ProjectCode { get; set; }
        public string? Description { get; set; }
        public DateTime ProjectCodeStartDate { get; set; }
        public DateTime ProjectCodeEndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class ProjectMaster
    {
        public int ProjectId { get; set; }
        public int ProjectManagerId { get; set; }
        public string? ProjectManagerName { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? AllocatedHours { get; set; }
        public string? ConsumedHours { get; set; }
        public string? ProjectCode { get; set; }
        public string? Description { get; set; }
        public DateTime ProjectCodeStartDate { get; set; }
        public DateTime ProjectCodeEndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public int Status { get; set; }
    }

    public class TimeClockingViewModel
    {
        public List<string>? Users { get; set; }
        public List<string>? Customers { get; set; }
        public List<string>? ClockingCodes { get; set; }
    }

    public class AttendanceRecord
    {
        public string? EmployeeName { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int SickLeaves { get; set; }
    }

    public class Attendence
    {
        public int AttendenceId { get; set; }
        public decimal TotalWorkingHours { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal AbsentHours { get; set; }
        public decimal ProductiveHours { get; set; }
        public decimal LeaveHours { get; set; }
        public decimal LateArrivalHours { get; set; }
        public int EmployeeId { get; set; }
        public string? AttendenceStatus { get; set; }
        public string? ResourceName { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public DateTime currentdate { get; set; }
        public string? WorkingHours { get; set; }

        public int Total { get; set; }
    }

    public class DepartmentMaster
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }

    public class LeaveMaster
    {
        public int LeaveID { get; set; }
        public string? LeaveType { get; set; }
        public int EmployeeID { get; set; }
        public string? Remarks { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string? LeaveReason { get; set; }
        public string? LeaveStatus { get; set; }
        public DateTime AppliedDate { get; set; }
        public int ApprovedBy { get; set; }
        public string? Approver { get; set; }
        public int Pending { get; set; }
        public int Approved { get; set; }
        public int Rejected { get; set; }
        public int Available { get; set; }
        public IFormFile? Attachment { get; set; } // New property for attachment
        public byte[]? AttachmentBytes { get; set; }
    }



}
