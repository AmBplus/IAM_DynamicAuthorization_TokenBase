
using AccessManagement.Data;
using AccessManagement.Entities;

using AccessManagement.Services;
using AccessManagement.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessManagement.Services.Permission.Command;
using AccessManagement.Services.Permission;
using AccessManagement.SeedData;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Security;
using Features.Common;

namespace AccessManagement.Services.Injection
{
    public static class ServiceInjection
    {
        public static IServiceCollection BootstrapServices(this IServiceCollection services,IConfiguration configuration)

        {
            // Add jwtSettings To IOC
            var jwtSettings = new JwtSettings();
            configuration.Bind("JWT", jwtSettings);
            services.AddSingleton(jwtSettings);
            // Get Connection String
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            //// Add Db Context
            //services.AddDbContext< AccessManagementDbContext>(options =>
            //    options.UseSqlServer(connectionString));
            services.AddDbContext<IAccessManagementDbContext,AccessManagementDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Adding Identity
            services.AddIdentity<UserEntity, RoleEntity>()
                .AddEntityFrameworkStores<AccessManagementDbContext>()
                .AddDefaultTokenProviders();

           services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.ValidIssuer,
            ValidAudience = jwtSettings.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };
    });
            // using Microsoft.Extensions.DependencyInjection;
            services
                .AddHttpContextAccessor();
            // **************************************************

            // **************************************************
            services.AddScoped
                <Features.Common.HttpContextService>();
            // **************************************************

            // **************************************************
            services.AddScoped
                <AuthenticatedUserService>();
            // **************************************************

            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPermissionHelper, PermissionHelper>();
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(RevokeTokenHandler).Assembly));
            services.AddTransient<AuthenticatedUserService>();
         
            services.AddScoped<SeedInitialData>();

            return services;
        }
    }
}
