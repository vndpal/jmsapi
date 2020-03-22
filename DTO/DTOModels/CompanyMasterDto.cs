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
        public string CompanyType { get; set; }
        public List<string> CompanyTypeValue { get; set; }
        public string FieldType { get; set; }
        public List<string> FieldTypeValue { get; set; }
        public string GSTNo { get; set; }
        public string PANNo { get; set; }
        public string AadharNo { get; set; }
        public string OwnerName { get; set; }
        public string CompanyAddress { get; set; }
        public string MobileNo { get; set; }
        public string AltMobileNo { get; set; }
        public string EmailId { get; set; }
        public int Status { get; set; }
        public string StatusValue { get; set; }
        public string IP { get; set; }
    }
}
