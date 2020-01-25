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

        public async Task<SingleReturnResult<string>> AddUpdateCompany(List<CompanyMasterDto> comp)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                //string result = "";
                DataTable dtCompany = _conn.ToDataTable(comp);
                dtCompany.Columns.Remove("CompanyId");

                object stat = _conn.ExecuteProcedure("InsertUpdateCompanyMaster", new SqlParameter("CompanyDetail", dtCompany));
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
                string SqlQuery = "SELECT * FROM CompanyMaster WHERE CompanyId = @CompanyId" ;
                
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    comp.result =await connection.QueryFirstOrDefaultAsync<CompanyMasterDto>(SqlQuery , new { CompanyId = Id });
                }
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

        public async Task<SingleReturnResult<CompanyMasterDto>> updateCompany(CompanyMasterDto companyDetails)
        {
            SingleReturnResult<CompanyMasterDto> comp = new SingleReturnResult<CompanyMasterDto>();
            try
            {
                string SqlQuery = "update CompanyMaster set CompanyName=@CompanyName,CompanyCode=@CompanyCode," +
                                    "CompanyType=@CompanyType,FieldType=@FieldType,GSTNo=@GSTNo,PANNo=@PANNo," +
                                    "AadharNo=@AadharNo,OwnerName=@OwnerName,CompanyAddress=@CompanyAddress," +
                                    "MobileNo=@MobileNo,AltMobileNo=@AltMobileNo,EmailId=@EmailId,Status=@Status " +
                                    "WHERE CompanyId = @CompanyId";

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    comp.result = await connection.QueryFirstOrDefaultAsync<CompanyMasterDto>(SqlQuery, new
                    { CompanyId = companyDetails.CompanyId,
                        CompanyName = companyDetails.CompanyName,
                        CompanyCode =companyDetails.CompanyCode,
                        CompanyType = companyDetails.CompanyCode,
                        FieldType =companyDetails.FieldType,
                        GSTNo = companyDetails.GSTNo,
                        PANNo= companyDetails.PANNo,
                        AadharNo = companyDetails.AadharNo,
                        OwnerName = companyDetails.OwnerName,
                        CompanyAddress = companyDetails.CompanyAddress,
                        MobileNo=companyDetails.MobileNo,
                        AltMobileNo=companyDetails.AltMobileNo,
                        EmailId=companyDetails.EmailId,
                        Status = companyDetails.Status
                    });
                }
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
    }
}
