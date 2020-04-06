using System;
using System.Collections.Generic;
using System.Text;
using static DTO.Common.Enums;

namespace DTO.DTOModels
{
   public class EmployeeDto
    {
        public int EmpId { get; set; }

        public string EmployeeCode { get; set; }

        public int DepartmentTypeId { get; set; }

        public int EmployeeTypeId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string ReferenceBy { get; set; }

        public int AddressProofId { get; set; }

        public int IdentityProofId { get; set; }

        public string AddressProof { get; set; }

        public string IdentityProof { get; set; }

        public string Photo { get; set; }

        public string MobileNo { get; set; }

        public string AlternateMobileNo { get; set; }

        public DateTime? JoiningDate { get; set; }

        public DateTime? ReleivingDate { get; set; }

        public int Status { get; set; }
    }
}
