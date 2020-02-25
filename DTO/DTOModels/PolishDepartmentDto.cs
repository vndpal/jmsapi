using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class PolishDepartmentDto
    {
        public int PolishId { get; set; }
        public int JobId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int PolishType { get; set; }
        public int CarratType { get; set; }
        public decimal? Hallmark { get; set; }
        public decimal? IssueWeight { get; set; }
        public decimal? ReceiveWeight { get; set; }
        public decimal? WeightLoss { get; set; }
        public decimal? Purity { get; set; }
        public string Remark { get; set; }
        public byte Status { get; set; }
    }
}
