using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class LoginDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string role { get; set; }
    }
}
