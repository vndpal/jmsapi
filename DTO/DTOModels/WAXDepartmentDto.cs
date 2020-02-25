using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class WAXDepartmentDto
    {
        public int WAXId { get; set; }
        public int JobId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? MouldWeight { get; set; }
        public decimal? MouldRate { get; set; }
        public decimal? MouldAmount { get; set; }
        public int MouldType { get; set; }
        public string Remark { get; set; }
        public byte Status { get; set; }
    }
}
