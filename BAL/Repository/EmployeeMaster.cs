using BLL.Interface;
using DTO.DTOModels;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class EmployeeMaster : IEmployeeMaster
    {
        private readonly IDbConnections _conn;
        public EmployeeMaster(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddUpdateEmployee(EmployeeDto employeeDto)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {

                object stat = _conn.ExecuteProcedure("InsertUpdateEmployeeMaster", new SqlParameter("DepartmentTypeId", employeeDto.DepartmentTypeId),
                                                                                    new SqlParameter("EmployeeTypeId", employeeDto.EmployeeTypeId),
                                                                                    new SqlParameter("FirstName", employeeDto.FirstName),
                                                                                    new SqlParameter("MiddleName", employeeDto.MiddleName),
                                                                                   new SqlParameter("LastName", employeeDto.LastName),
                                                                                   new SqlParameter("EmailId", employeeDto.EmailId),
                                                                                  new SqlParameter("Address", employeeDto.Address),
                                                                                  new SqlParameter("ReferenceBy", employeeDto.ReferenceBy),
                                                                                 new SqlParameter("AddressProofId", employeeDto.AddressProofId),
                                                                                 new SqlParameter("IdentityProofId", employeeDto.IdentityProofId),
                                                                                 new SqlParameter("AddressProof", employeeDto.AddressProof),
                                                                                 new SqlParameter("IdentityProof", employeeDto.IdentityProof),
                                                                                 new SqlParameter("Photo", employeeDto.Photo),
                                                                                 new SqlParameter("MobileNo", employeeDto.MobileNo),
                                                                                 new SqlParameter("AlternateMobileNo", employeeDto.AlternateMobileNo),
                                                                                 new SqlParameter("JoiningDate", employeeDto.JoiningDate),
                                                                                 new SqlParameter("ReleavingDate", employeeDto.ReleivingDate),
                                                                                 new SqlParameter("Status", employeeDto.Status));
                if (stat != null)
                {
                    result.Flag = ApplicationConstants.successFlag;
                    result.message = "Data Inserted Successfully";
                    result.result = "Ok";
                }
                else
                {
                    result.Flag = ApplicationConstants.failureFlag;
                    result.message = "some error has occured while inserting the data";
                    result.result = "";
                }

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                result.Flag = ApplicationConstants.failureFlag;
                result.message = ex.ToString();
                return result;
            }
        }

        public Task<ListReturnResult<EmployeeDto>> GetAllCompany()
        {
            throw new NotImplementedException();
        }
    }
}
