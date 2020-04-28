using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class AssignedJobDTO
    {
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public string JobNo { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public decimal StockWeight { get; set; }
        public int HallmarkId { get; set; }
        public string Hallmark { get; set; }
        public string Employee { get; set; }
        public int Status { get; set; }
        public string StatusValue { get; set; }

    }
}
