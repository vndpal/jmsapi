using BLL.Interface;
using Dapper;
using DTO.DTOModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class LoginMaster : ILoginMaster
    {
        private readonly IDbConnections _conn;
        private readonly IConfiguration _config;

        public LoginMaster(IDbConnections conn, IConfiguration config)
        {
            _conn = conn;
            _config = config;
        }

        public async  Task<SingleReturnResult<string>> AuthenticateUser(LoginDto login)
        {
            SingleReturnResult<string> result = new SingleReturnResult<string>();
            try
            {
                string SqlQuery = "SELECT * FROM Login_Mst WHERE username = @Username and password=@Password";

                LoginDto loginDetails = new LoginDto();
                using (var connection = new SqlConnection(_conn.strConnectionString()))
                {
                    await connection.OpenAsync();
                    loginDetails = await connection.QueryFirstOrDefaultAsync<LoginDto>(SqlQuery, new { Username = login.username, Password = login.password });
                }
                if (loginDetails != null)
                {
                    result.Flag = ApplicationConstants.successFlag;
                    result.message = "Login Successfull !";
                    result.result = GenerateJSONWebToken(loginDetails);
                }
                else
                {
                    result.Flag = ApplicationConstants.failureFlag;
                    result.message = "Incorrect Username or password !";
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

        public async Task<SingleReturnResult<LoginDto>> RegisterUser(LoginDto userDetails)
        {
            SingleReturnResult<LoginDto> result = new SingleReturnResult<LoginDto>();

            try
            {
                return result;
            }
            catch (Exception ex)
            {
                result.Flag = ApplicationConstants.failureFlag;
                result.message = "Some error has occured while registering the user, please try again after sometime";
                return result;
            }
        }

        //public async  Task<SingleReturnResult<LoginDto> RegisterLogin(LoginDto login)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.OpenAsync();
        //        var query = @"
        //                    SELECT 
        //                    Roleid
        //                    FROM ROLE_MST WHERE RoleValue = upper(@RoleValue)";


        //        var RoleId = connection.QueryFirstOrDefault<int>(query, new { RoleValue = "ADMIN" });

        //        var sqlStatement = @"
        //                            INSERT INTO login_mst 
        //                            (username
        //                            ,password
        //                            ,email
        //                            ,mobile)
        //                            VALUES (@username
        //                            ,@password
        //                            ,@email
        //                            ,@mobile)";
        //        connection.ExecuteAsync(sqlStatement, login);
        //    }
        //    LoginDto sd = new LoginDto();
        //    return sd;
        //}

        public string GenerateJSONWebToken(LoginDto login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, login.username),
            new Claim(JwtRegisteredClaimNames.Email, ""),
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
    }
}
