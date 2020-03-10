using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using BLL.Repository;
using DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Entities;

namespace JMS_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(optins =>
                {
                    optins.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://example.com",
                                        "http://localhost:3000").AllowAnyHeader()
                                .AllowAnyMethod();
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton(Configuration);
            //services.Configure<AppSettingsDto>(Configuration).AddSingleton(sp => sp.GetRequiredService<IOptions<AppSettingsDto>>().Value);
            services.Configure<AppSettingsDto>(Configuration);
            services.AddScoped<IDbConnections, DbConnection>();
            services.AddScoped<ICompanyMaster , CompanyMaster>();
            services.AddScoped<IEmployeeMaster, EmployeeMaster>();
            services.AddScoped<IJobMaster, JobMaster>();
            services.AddScoped<IHRDepartment, HRDepartment>();
            services.AddScoped<ICADDepartment, CADDepartment>();
            services.AddScoped<ICASTDepartment, CASTDepartment>();
            services.AddScoped<IWAXDepartment, WAXDepartment>();
            services.AddScoped<IPolishDepartment, PolishDepartment>();
            services.AddScoped<IFillingDepartment, FillingDepartment>();
            services.AddScoped<ILoginMaster, LoginMaster>();
            services.AddScoped<ICommonMaster, CommonMaster>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc();
        }
    }
}
