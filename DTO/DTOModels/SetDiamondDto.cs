﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class SetDiamondDto
    {
        public int JobId { get; set; }
        public int DiaId { get; set; }
        public int Studded { get; set; }
        public decimal StuddedWeight { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public int BalancePiece { get; set; }
        public decimal BalanceWeight { get; set; }
        public int BrokenPiece { get; set; }
        public decimal BrokenWeight { get; set; }
      
    }
}
