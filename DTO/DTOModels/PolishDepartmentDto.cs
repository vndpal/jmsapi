using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class PolishDepartmentDto
    {
        public int PolishId { get; set; }
        public int JobId { get; set; }
        public string JobNo { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int PolishType { get; set; }
        public string PolishTypeValue { get; set; }
        public decimal? IssuedWeight { get; set; }
        public decimal? ReceivedWeight { get; set; }
        public decimal? WeightLoss { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public string StatusValue { get; set; }
    }

    public class PolishReportDto
    {
        public string JobNo { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string PolishType { get; set; }
        public decimal? IssuedWeight { get; set; }
        public decimal? ReceivedWeight { get; set; }
        public decimal? WeightLoss { get; set; }
        public string Status { get; set; }
    }
}
