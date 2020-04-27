using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class DiamondDetailDto
    {
        public int DiaId { get; set; }
        public int JobId { get; set; }
        public DateTime? DiamondDate { get; set; }
        public int DiamondType { get; set; }
        public string DiamondTypeValue { get; set; }
        public int InventoryType { get; set; }
        public string Description { get; set; }
        public int IssuedPiece { get; set; }
        public decimal IssuedWeight { get; set; }
    }
}
