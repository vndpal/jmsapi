using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class CASTDepartmentDto
    {
        public int CastId { get; set; }
        public int JobId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public int Material { get; set; }
        public int MaterialType { get; set; }
        public int Hallmark { get; set; }
        public decimal? CastReceivedWeight { get; set; }
        public decimal? CastRate { get; set; }
        public decimal? CastAmount { get; set; }
        public string Remark { get; set; }
        public byte Status { get; set; }
    }
}
