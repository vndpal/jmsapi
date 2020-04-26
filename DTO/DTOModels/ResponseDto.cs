using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class ResponseDto
    {
        public long UserId { get; set; }
        public string FirtName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public long RoleId { get; set; }
        public string Token { get; set; }
    }
}
