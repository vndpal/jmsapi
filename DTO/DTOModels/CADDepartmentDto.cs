using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class CADDepartmentDto
    {
        public int CADId { get; set; }
        public int JobId { get; set; }
        public string JobNo { get; set; }
        public int EmployeeId { get; set; }
        public string Employee { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public int Quantity { get; set; }
        public int ResinType { get; set; }
        public decimal? RPTWeight { get; set; }
        public decimal RPTRate { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        //public DateTime? AddedOn { get; set; }
        //public int AddedBy { get; set; }
        //public DateTime? ModifiedOn { get; set; }
        //public int ModifiedBy { get; set; }
        public byte Status { get; set; }

    }
}
