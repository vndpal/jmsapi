using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class StockDto
    {
        public int StockId { get; set; }
        public int JobId { get; set; }
        public DateTime StockDate { get; set; }
        public decimal StockWeight { get; set; }
        public decimal Purity { get; set; }
        public string Remark { get; set; }
        public DateTime AddedOn { get; set; }
        public int AddedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
    }
}
