using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class CADDepartmentDto
    {
        public int CADId { get; set; }
        public int JobId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public int PieceQuantity { get; set; }
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
