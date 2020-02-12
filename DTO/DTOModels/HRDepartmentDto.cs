using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class HRDepartmentDto
    {
        public int JobId { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? IssuedDate { get; set; }
        public int IssuedPieces { get; set; }
        public decimal IssuedWeight { get; set; }
        public string Remark { get; set; }
    }
}
