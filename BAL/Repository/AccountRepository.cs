using BLL.Interface;
using Dapper;
using DTO.DTOModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnections _conn;
        private readonly IConfiguration _config;

        public AccountRepository(IDbConnections conn, IConfiguration config)
        {
            _conn = conn;
            _config = config;
        }

        public async Task<SingleReturnResult<ResponseDto>> Login(LoginDto loginDetails)
        {
            SingleReturnResult<ResponseDto> result = new SingleReturnResult<ResponseDto>();
            try
            {
                var SqlQuery = "[sp_GetUserByEmailId]";
                var values = new { EmailId = loginDetails.EmailId };

                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();

                    var user = await connection.QueryFirstOrDefaultAsync<UserMasterModel>(SqlQuery, values, commandType: CommandType.StoredProcedure);

                    if (user == null)
                    {
                        result.Flag = ApplicationConstants.failureFlag;
                        result.message = "Email is NOT valid";
                        return result;
                    }

                    if (!VerifyPasswordHash(loginDetails.Password, user.PasswordHash, user.PasswordSalt))
                    {
                        result.Flag = ApplicationConstants.failureFlag;
                        result.message = "Password is NOT valid";
                        return result;
                    }

                    if (user != null)
                    {
                        result.Flag = ApplicationConstants.successFlag;
                        result.message = "Login Successfull !";
                        result.returnId = user.Id;
                        ResponseDto dto = new ResponseDto()
                        {
                            Token = GenerateJSONWebToken(user),
                            UserId = user.Id,
                            FirtName = user.FirstName,
                            LastName = user.LastName,
                            EmailId = user.EmailId,
                            RoleId = user.RoleId
                        };
                        result.result = dto;
                    }
                    else
                    {
                        result.Flag = ApplicationConstants.failureFlag;
                        result.message = "Incorrect Username or password !";
                    }

                }

                return result;
            }
            catch (Exception ex)
            {
                result.Flag = ApplicationConstants.failureFlag;
                result.message = ex.ToString();
                return result;
            }
        }

        public string GenerateJSONWebToken(UserMasterModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub,user.FirstName ),
            new Claim(JwtRegisteredClaimNames.Email, user.EmailId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //matching each char of password hash 
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) { return false; }
                }
            }
            return true;
        }

        public async Task<SingleReturnResult<string>> Register(UserMasterModel userDetails)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                byte[] passwordHash, passwordSalt;
                // Check for need to hash password
                if (userDetails.Id == 0)
                {
                    // Creating PasswordHash and PasswordSalt for password.
                    // PasswordHash and PasswordSalt get update in this method 
                    // out with passwoordHash and Salt will provid updated value.
                    CreatePasswordHash(userDetails.Password, out passwordHash, out passwordSalt);
                    userDetails.PasswordHash = passwordHash;
                    userDetails.PasswordSalt = passwordSalt;
                }
                var state = _conn.ExecuteProcedure("sp_RegisterUser",
                   new SqlParameter("Id", userDetails.Id),
                   new SqlParameter("RoleId", userDetails.RoleId),
                   new SqlParameter("FirstName", userDetails.FirstName),
                   new SqlParameter("MiddleName", userDetails.MiddleName),
                   new SqlParameter("LastName", userDetails.LastName),
                   new SqlParameter("EmailId", userDetails.EmailId),
                   new SqlParameter("PasswordHash", userDetails.PasswordHash),
                   new SqlParameter("PasswordSalt", userDetails.PasswordSalt),
                   new SqlParameter("MobileNo", userDetails.MobileNo),
                   new SqlParameter("CreatedBy", userDetails.CreatedBy),
                   new SqlParameter("CreatedOn", DateTime.Now),
                   new SqlParameter("ModifyBy", userDetails.ModifyBy),
                   new SqlParameter("ModifyOn", DateTime.Now),
                   new SqlParameter("Status", userDetails.Status));

                if (state != null && state.ToString() != "Email Id already exists" && state.ToString() != "Mobile Number already exists")
                {
                    result.Flag = ApplicationConstants.successFlag;
                    result.message = "Data Inserted Successfully";
                    // result.returnId = state;
                    result.result = "Ok";
                }
                else
                {
                    result.message = state != null ? state.ToString() : "some error has occured while inserting the data";
                    result.Flag = ApplicationConstants.failureFlag;
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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        public async Task<ListReturnResult<UserMasterModel>> GetUsers()
        {
            ListReturnResult<UserMasterModel> users = new ListReturnResult<UserMasterModel>();
            try
            {
                // Store Porcedure name
                string SqlQuery = "sp_GetUsers";
                // Getting connection info
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    // Opening connection string
                    await connection.OpenAsync();
                    // Getting all users form table
                    users.result = connection.Query<UserMasterModel>(SqlQuery).AsList();
                }
                users.Flag = ApplicationConstants.successFlag;
                users.message = "Data Fetched successfully";
                return users;
            }
            catch (Exception ex)
            {
                users.Flag = ApplicationConstants.failureFlag;
                users.message = ex.ToString();
                return users;
            }
        }

        public async Task<SingleReturnResult<UserMasterModel>> GetUserById(long id)
        {
            SingleReturnResult<UserMasterModel> user = new SingleReturnResult<UserMasterModel>();
            try
            {
                // Store Procedure name
                string SqlQuery = "sp_GetUserById";
                // Getting connection string info 
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    // Opening connection string
                    await connection.OpenAsync();
                    // Getting user info by id
                    user.result = await connection.QueryFirstOrDefaultAsync<UserMasterModel>(SqlQuery, new { Id = id }, commandType: CommandType.StoredProcedure);
                }
                user.Flag = ApplicationConstants.successFlag;
                user.message = "Data Fetched Successfully!";
                return user;
            }
            catch (Exception ex)
            {
                user.Flag = ApplicationConstants.failureFlag;
                user.message = ex.ToString();
                return user;
            }
        }

        public async Task<SingleReturnResult<int>> SetRoleMenuMapping(RoleMenuModel roleMenu)
        {
            SingleReturnResult<int> result = new SingleReturnResult<int>();
            try
            {
                // Stored procedure name
                string sqlQuery = "sp_InsertUpdateRoleMenuMapping";
                // Storing list of data inot datatable
                DataTable dtMenu = _conn.ToDataTable(roleMenu.MenuList);
                // Getting connection string config
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    // Opening connection string
                    await connection.OpenAsync();
                    var param = new DynamicParameters();
                    // Adding parameter
                    param.Add("@RoleId", roleMenu.RoleId, DbType.Int32);
                    // Making DBType as UDT 
                    param.Add("@MenuList", dtMenu.AsTableValuedParameter("[dbo].[udt_MenuList]"));
                    // Updating Roleid, MenuId and Status in mapping table
                    result.result = await connection.QueryFirstOrDefaultAsync<int>(sqlQuery, param, commandType: CommandType.StoredProcedure);
                }
                result.Flag = ApplicationConstants.successFlag;
                result.message = "Data Fetched Successfully!";
                return result;
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
