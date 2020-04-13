using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class InventoryDto
    {
        public int InvId { get; set; }
        public DateTime InvDate { get; set; }
        public int Company { get; set; }
        public string MetalType { get; set; }
        public float MetalWeight { get; set; }
        public int Hallmark { get; set; }
        public float Purity { get; set; }
        public string Remark { get; set; }
        public DateTime AddedOn { get; set; }
        public int AddedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
    }
}
