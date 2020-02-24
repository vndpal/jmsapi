using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class FillingDepartmentDto
    {
        public int FillingId { get; set; }
        public int JobId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int Material { get; set; }
        public int MaterialType { get; set; }
        public int Hallmark { get; set; }
        public decimal? IssueWeight { get; set; }
        public decimal? RawGhatWeight { get; set; }
        public decimal? PercentageLoss { get; set; }
        public decimal? Loss { get; set; }
        public string Remark { get; set; }
        public byte Status { get; set; }
    }
}
