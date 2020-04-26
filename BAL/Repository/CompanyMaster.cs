using BLL.Interface;
using Dapper;
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
    public class CompanyMaster : ICompanyMaster
    {
        private readonly IDbConnections _conn;
        public CompanyMaster(IDbConnections conn)
        {
            _conn = conn;
        }

        public async Task<SingleReturnResult<string>> AddCompany(CompanyMasterDto comp)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
                //DataTable dtCompany = _conn.ToDataTable(comp);
                //dtCompany.Columns.Remove("CompanyId");

                object stat = _conn.ExecuteProcedure("InsertUpdateCompanyMaster", new SqlParameter("Type", "Insert")
                                                                                , new SqlParameter("CompanyId", comp.CompanyId)
                                                                                , new SqlParameter("CompanyName", comp.CompanyName)
                                                                                , new SqlParameter("CompanyCode", comp.CompanyCode)
                                                                                , new SqlParameter("CompanyType", comp.CompanyType)
                                                                                , new SqlParameter("FieldType", comp.FieldType)
                                                                                , new SqlParameter("GSTNo", comp.GSTNo)
                                                                                , new SqlParameter("PANNo", comp.PANNo)
                                                                                , new SqlParameter("AadharNo", comp.AadharNo)
                                                                                , new SqlParameter("OwnerName", comp.OwnerName)
                                                                                , new SqlParameter("CompanyAddress", comp.CompanyAddress)
                                                                                , new SqlParameter("MobileNo", comp.MobileNo)
                                                                                , new SqlParameter("AltMobileNo", comp.AltMobileNo)
                                                                                , new SqlParameter("EmailId", comp.EmailId)
                                                                                , new SqlParameter("Status", comp.Status));
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

        public async Task<ListReturnResult<CompanyMasterDto>> GetAllCompany()
        {
            ListReturnResult<CompanyMasterDto> comp = new ListReturnResult<CompanyMasterDto>();
            try
            {
                string SqlQuery = "SELECT * FROM CompanyMaster";
                
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    comp.result = connection.Query<CompanyMasterDto>(SqlQuery).AsList();
                }
                comp.Flag = ApplicationConstants.successFlag;
                comp.message = "Data Fetched successfully";
                return comp;
            }
            catch (Exception ex)
            {
                comp.Flag = ApplicationConstants.failureFlag;
                comp.message = ex.ToString();
                return comp;
            }

        }

        public async Task<SingleReturnResult<CompanyMasterDto>> GetCompany(int Id)
        {
            SingleReturnResult<CompanyMasterDto> comp = new SingleReturnResult<CompanyMasterDto>();
            try
            {
                DataTable dtComp = await _conn.ExecuteProcedureForDataTable("GetCompanyById", new SqlParameter("CompanyId", Id));

               
                string companyType = dtComp.Rows[0]["CompanyType"].ToString();
                string[] compTypeId = companyType.Split(',');
                List<string> ObjectComapnyType = new List<string>();
                for (int i = 0; i < compTypeId.Length; i++)
                {
                    object companyTypeValue = _conn.ExecuteProcedure("GetCompanyById", new SqlParameter("CompanyTypeId", compTypeId[i]));
                    if (companyTypeValue == null)
                    {
                        companyTypeValue = string.Empty;
                    }
                    ObjectComapnyType.Add(companyTypeValue.ToString());
                }

                string fieldType = dtComp.Rows[0]["FieldType"].ToString();
                string[] fieldTypeId = companyType.Split(',');
                List<string> ObjectFieldType = new List<string>();
                for (int i = 0; i < fieldTypeId.Length; i++)
                {
                    object fieldTypeValue = _conn.ExecuteProcedure("GetCompanyById", new SqlParameter("FieldTypeId", fieldTypeId[i]));
                    if (fieldTypeValue == null)
                    {
                        fieldTypeValue = string.Empty;
                    }
                    ObjectFieldType.Add(fieldTypeValue.ToString());
                }


                comp.result = _conn.ConvertDataTable<CompanyMasterDto>(dtComp)[0];
                comp.result.CompanyTypeValue = ObjectComapnyType;
                comp.result.FieldTypeValue = ObjectFieldType;
                comp.Flag = ApplicationConstants.successFlag;
                comp.message = "Data Fetched Successfully!";
                return comp;
            }
            catch (Exception ex)
            {
                comp.Flag = ApplicationConstants.failureFlag;
                comp.message = ex.ToString();
                return comp;
            }
        }

        public async Task<SingleReturnResult<string>> UpdateCompany(CompanyMasterDto comp)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
                //DataTable dtCompany = _conn.ToDataTable(comp);
                //dtCompany.Columns.Remove("CompanyId");

                object stat = _conn.ExecuteProcedure("InsertUpdateCompanyMaster", new SqlParameter("Type", "Update")
                                                                                , new SqlParameter("CompanyId", comp.CompanyId)
                                                                                , new SqlParameter("CompanyName", comp.CompanyName)
                                                                                , new SqlParameter("CompanyCode", comp.CompanyCode)
                                                                                , new SqlParameter("CompanyType", comp.CompanyType)
                                                                                , new SqlParameter("FieldType", comp.FieldType)
                                                                                , new SqlParameter("GSTNo", comp.GSTNo)
                                                                                , new SqlParameter("PANNo", comp.PANNo)
                                                                                , new SqlParameter("AadharNo", comp.AadharNo)
                                                                                , new SqlParameter("OwnerName", comp.OwnerName)
                                                                                , new SqlParameter("CompanyAddress", comp.CompanyAddress)
                                                                                , new SqlParameter("MobileNo", comp.MobileNo)
                                                                                , new SqlParameter("AltMobileNo", comp.AltMobileNo)
                                                                                , new SqlParameter("EmailId", comp.EmailId)
                                                                                , new SqlParameter("Status", comp.Status));
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
    }
}
