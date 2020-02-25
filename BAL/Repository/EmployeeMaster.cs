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

        public async Task<SingleReturnResult<string>> AddUpdateEmployee(List<EmployeeDto> employeeDto)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
                DataTable dtEmpolyee = _conn.ToDataTable(employeeDto);
                //List<StoreProcedureParameter> storeProcedureParameters = new List<StoreProcedureParameter>();
                //dtCompany.Columns.Remove("CompanyId");

                object stat = _conn.ExecuteProcedure("InsertOrUpdateEmpMaster", new SqlParameter("EmpDetail", dtEmpolyee));
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
