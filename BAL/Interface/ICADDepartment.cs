using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICADDepartment
    {
        Task<SingleReturnResult<string>> AddCAD(List<CADDepartmentDto> cad);
        Task<ListReturnResult<CADDepartmentDto>> GetAllCAD();
        Task<SingleReturnResult<CADDepartmentDto>> GetCAD(int Id);
        Task<SingleReturnResult<CADDepartmentDto>> UpdateCAD(CADDepartmentDto caddetails);
    }
}
