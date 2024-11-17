using BusinessLogicLayer.IServices;
using BusinessModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.IRepositories;

namespace BusinessLogicLayer.Services
{
    public class AttendanceService : IAttendanceService
    {
        string? _connectionString = string.Empty;
        private readonly IAttendenceRepository _IAttendenceRepository;
        public AttendanceService(IConfiguration configuration, IAttendenceRepository iAttendenceRepository)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _IAttendenceRepository = iAttendenceRepository;
        }

        // method to get attendance data by resourceId
        public Attendence GetAllAttendenceHoursByEmployeeIdService(int EmployeeId)
        {
            return _IAttendenceRepository.GetAllAttendenceHoursByEmployeeIdRepository(EmployeeId);
        }

        // New method to get attendance details for a month
        public List<DateTime> GetDatesForMonth(int year, int month)
        {
            List<DateTime> monthDates = new List<DateTime>();

            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1); // Last day of the month

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                monthDates.Add(date);
            }

            return monthDates;
        }

        // New method to get present/absent days for an employee
        public (int Present, int Absent) GetAttendenceStatusForMonth(int resourceId)
        {
            var attendenceList = _IAttendenceRepository.GetAttendenceListByResourceIdRepository(resourceId);

            int present = attendenceList.Where(c => c.Present == 1).Select(c => c.Total).FirstOrDefault();
            int absent = attendenceList.Where(c => c.Present == 0).Select(c => c.Total).FirstOrDefault();

            return (present, absent);
        }

        // New method to get employee profile details
        public List<EmployeeProfileDetails> GetEmployeeProfileDetails(int employeeId)
        {
            return _IAttendenceRepository.GetAllEmployeesRepository().Where(c => c.Id == employeeId).ToList();
        }

        public List<EmployeeProfileDetails> GetAllEmployeesService()
        {
            return _IAttendenceRepository.GetAllEmployeesRepository();
        }
        public List<Attendence> GetAttendenceListByResourceIdService(int resourceId)
        {
            return _IAttendenceRepository.GetAttendenceListByResourceIdRepository(resourceId);
        }
        public Attendence GetAttendenceByResourceIdService(int id)
        {
            return _IAttendenceRepository.GetAttendenceByResourceIdRepository(id);
        }
        public Attendence GetAttendenceDetailsbYEmployeeIdAndCurrentDateService(int resourceid, DateTime date)
        {
            return _IAttendenceRepository.GetAttendenceDetailsbYEmployeeIdAndCurrentDateRepository(resourceid, date);
        }
        public Attendence GetAttendenceStatusByDateAndResourceIdService(int resourceid, DateTime date)
        {
            return _IAttendenceRepository.GetAttendenceStatusByDateAndResourceIdRepository(resourceid, date);
        }
    }
}
