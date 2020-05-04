using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class SettingDepartmentDto
    {
        public int SettingId { get; set; }
        public int JobId { get; set; }
        public string JobNo { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public decimal IssuedWeight { get; set; }
        public decimal ReceivedWeight { get; set; }
        public decimal DiamondWeight { get; set; }
        public decimal Loss { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public string StatusValue { get; set; }
        public List<SetDiamondsDto> SetDiamond { get; set; }
    }

}
