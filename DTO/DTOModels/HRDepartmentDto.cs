using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class HRDepartmentDto
    {
        public int HRId { get; set; }
        public int JobId { get; set; }
        public string JobNo { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int EmployeeId { get; set; }
        public string Employee { get; set; }
        public DateTime? IssuedDate { get; set; }
        public decimal IssuedWeight { get; set; }
        public string Remark { get; set; }
    }

    public class HRReportDto
    {
        public string JobNo { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public string EmployeeName { get; set; }
        public decimal IssuedWeight { get; set; }
        public DateTime? IssuedDate { get; set; }
    }
}
