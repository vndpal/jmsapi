using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class UserMasterModel
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string MobileNo { get; set; }

        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public long Id { get; set; }

        public long RoleId { get; set; }

        public long CreatedBy { get; set; }

        public System.DateTime CreatedOn { get; set; }

        public Nullable<long> ModifyBy { get; set; }

        public Nullable<System.DateTime> ModifyOn { get; set; }

        public bool Status { get; set; }
    }
}
