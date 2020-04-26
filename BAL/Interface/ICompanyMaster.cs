using DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ICompanyMaster
    {
       Task<SingleReturnResult<string>> AddCompany(CompanyMasterDto comp);
       Task<ListReturnResult<CompanyMasterDto>> GetAllCompany();
       Task<SingleReturnResult<CompanyMasterDto>> GetCompany(int Id);
        Task<SingleReturnResult<string>> UpdateCompany(CompanyMasterDto comp);
    }
}
