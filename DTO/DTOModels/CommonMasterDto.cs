using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class CommonMasterDto
    {
        public int Id { get; set; }
        public string Masterkey { get; set; }
        public string MasterValue { get; set; }
        public string MasterGroup { get; set; }
        public int HierachyKey { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
