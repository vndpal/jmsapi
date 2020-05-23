using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class DeliveryScreenDto
    {
        public int DeliveryId { get; set; }
        public int JobId { get; set; }
        public string JobNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string BookNo { get; set; }
        public decimal? GrossWeight { get; set; }
        public decimal? NetWeightPure { get; set; }
        public decimal? NetWeight24K { get; set; }
        public string Remark { get; set; }
    }

}
