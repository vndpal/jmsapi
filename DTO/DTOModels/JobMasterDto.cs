using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class JobMasterDto
    {
        public int JobId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string JobNo { get; set; }
        public string ClientJobNo { get; set; }
        public int JobType { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int WorkType { get; set; }
        public string WorkTypeValue { get; set; }
        public string Remark { get; set; }
        public int ProcessStatus { get; set; }
        public int IsNewProcess { get; set; }
        public string DesignImage { get; set; }
        public List<DiamondDetailDto> diamondDetail { get; set; }
    }
}
