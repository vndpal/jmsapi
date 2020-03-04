using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class HRDepartmentDto
    {
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
}
