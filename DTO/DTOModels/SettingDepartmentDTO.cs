using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class SettingDepartmentDto
    {
        public int JobId { get; set; }
        public string JobNo { get; set; }
        public int EmployeeId { get; set; }
        public string Employee { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public decimal IssuedWeight { get; set; }
        public decimal ReceivedWeight { get; set; }
        public decimal DiamondWeight { get; set; }
        public decimal Loss { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public List<SetDiamondDto> SetDiamond { get; set; }
    }

}
