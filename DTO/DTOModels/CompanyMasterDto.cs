using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class CompanyMasterDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public int CompanyType { get; set; }
        public int FieldType { get; set; }
        public string GSTNo { get; set; }
        public string PANNo { get; set; }
        public string AadharNo { get; set; }
        public string OwnerName { get; set; }
        public string CompanyAddress { get; set; }
        public string MobileNo { get; set; }
        public string AltMobileNo { get; set; }
        public string EmailId { get; set; }
        public int Status { get; set; }
        public string IP { get; set; }
    }
}
