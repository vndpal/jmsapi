using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class FillingDepartmentDto
    {
        public int FillingId { get; set; }
        public int JobId { get; set; }
        public string JobNo { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? IssuedWeight { get; set; }
        public decimal? RawGhatWeight { get; set; }
        public decimal? PercentageLoss { get; set; }
        public decimal? Loss { get; set; }
        public decimal? ExtraLoss { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public string StatusValue { get; set; }
    }
}
