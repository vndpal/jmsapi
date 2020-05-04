using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.DTOModels
{
    public class MenuDto
    {
        public int menuId { get; set; }
        public string menu { get; set; }
        public int hierarchy { get; set; }
        public int LevelFlag { get; set; }
        public int? subMenuId { get; set; }
    }

    public class MenuListDto
    {
        public int menuId { get; set; }
        public string menu { get; set; }
        public int hierarchy { get; set; }
        public int LevelFlag { get; set; }
        public int? subMenuId { get; set; }
        public List<MenuDto> subMens { get; set; }
    }
}
