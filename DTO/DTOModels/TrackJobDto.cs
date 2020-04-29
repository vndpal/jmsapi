using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class TrackJobDto
    {
        public int JobId { get; set; }
        public string JobNo { get; set; }
        public int StatusId { get; set; }
        public int FromDepartmentId { get; set; }
        public string FromDepartmentValue { get; set; }
        public int ToDepartmentId { get; set; }
        public string ToDepartmentValue { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal IssuedWeight { get; set; }
        public decimal ReceivedWeight { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
    }
}
