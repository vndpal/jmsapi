using System;
using System.Collections.Generic;
using System.Text;
using static DTO.Common.Enums;

namespace DTO.DTOModels
{
   public class EmployeeDto
    {
        public DepartmentType DepartmentType { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string RefrenceBy { get; set; }

        public AddresAndIdentity AddressProof { get; set; }

        public AddresAndIdentity IdentityProof { get; set; }

        public string Photo { get; set; }

        public string MobileNo { get; set; }

        public string AlternateMobileNo { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime ReleivingDate { get; set; }

        public bool Status { get; set; }
    }
}
