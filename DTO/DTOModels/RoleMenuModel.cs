using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class RoleMenuModel
    {
        public long RoleId { get; set; }

        public List<Menu> MenuList { get; set; }
    }


    public class Menu
    {
        public int MenuId { get; set; }

        public bool Status { get; set; }
    }
}
