using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class TouchUpDto
    {
        public int ExtraId { get; set; }
        public int JobId { get; set; }
        public string JobNo { get; set; }
        public DateTime? TouchUpDate { get; set; }
        public int MeenaType { get; set; }
        public decimal MeenaWeight { get; set; }
        public decimal MeenaAmount { get; set; }
        public decimal PaintingWeight { get; set; }
        public decimal PaintingAmount { get; set; }
        public decimal MotiAmount { get; set; }
        public decimal IssueChainWeight { get; set; }
        public decimal UsedChainWeight { get; set; }
        public decimal BalanceChainWeight { get; set; }
        public string Remark { get; set; }
        public List<SetDiamondsDto> TouchUpDetail { get; set; }
    }

}
