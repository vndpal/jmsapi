using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class TrackiJobDto
    {
        public int JobId { get; set; }
        public int StatusId { get; set; }
        public string Department { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public decimal IssueWeight { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime crtDate { get; set; }
    }
}
