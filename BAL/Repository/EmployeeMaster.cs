using BLL.Interface;
using Dapper;
using DTO.DTOModels;
using Microsoft.AspNetCore.Http;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

        public async Task<SingleReturnResult<string>> AddUpdateEmployee(EmployeeDto employeeDto,IFormFileCollection files)
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
                                                                                 //new SqlParameter("AddressProof", employeeDto.AddressProof),
                                                                                 //new SqlParameter("IdentityProof", employeeDto.IdentityProof),
                                                                                 //new SqlParameter("Photo", employeeDto.Photo),
                                                                                 new SqlParameter("AddressProof", "addrewsspo"),
                                                                                 new SqlParameter("IdentityProof", "addrewsspo"),
                                                                                 new SqlParameter("Photo", "addrewsspo"),
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

                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                saveFile(fileBytes);
                                //string s = Convert.ToBase64String(fileBytes);
                                // act on the Base64 data
                            }
                        }
                    }
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

        public async Task<ListReturnResult<EmployeeDto>> GetAllEmployee()
        {
            ListReturnResult<EmployeeDto> emp = new ListReturnResult<EmployeeDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Employees";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    emp.result = connection.Query<EmployeeDto>(SqlQuery).AsList();
                }
                emp.Flag = ApplicationConstants.successFlag;
                emp.message = "Data Fetched successfully";
                return emp;
            }
            catch (Exception ex)
            {
                emp.Flag = ApplicationConstants.failureFlag;
                emp.message = ex.ToString();
                return emp;
            }
        }

        public async Task<SingleReturnResult<EmployeeDto>> GetEmployee(int Id)
        {
            SingleReturnResult<EmployeeDto> emp = new SingleReturnResult<EmployeeDto>();
            try
            {
                string SqlQuery = "SELECT * FROM Employees WHERE EmpId = @EmpId";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    emp.result = await connection.QueryFirstOrDefaultAsync<EmployeeDto>(SqlQuery, new { EmpId = Id });
                }
                emp.Flag = ApplicationConstants.successFlag;
                emp.message = "Data Fetched Successfully!";
                return emp;
            }
            catch (Exception ex)
            {
                emp.Flag = ApplicationConstants.failureFlag;
                emp.message = ex.ToString();
                return emp;
            }
        }

        public void saveFile(byte[] file)
        {
            using (FileStream files = new FileStream(@"C:\test\12.png", FileMode.Create,FileAccess.ReadWrite))
            {
                files.Write(file, 0, file.Length);
            }
        }
    }
}
