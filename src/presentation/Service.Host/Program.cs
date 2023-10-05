

using Base.Shared.ResultUtility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AccessManagement.Services.Injection;
using AccessManagement.Controllers;
using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using MediatR;
using AccessManagement.Services.Permission.Command;
using Infrastructure.Security;
using AccessManagement.SeedData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.BootstrapServices(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers
    (x=>x.Filters.Add(new CustomAuthorizeAttribute()))
.
  AddApplicationPart(typeof(AuthenticateController).GetTypeInfo().Assembly).AddControllersAsServices()
 
    ; 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});



var app = builder.Build();
app.UseDeveloperExceptionPage();    


bool flag = true;
if (flag)
{
    using (var scope = app.Services.CreateScope())
    {
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>;
        var seeDataClass = scope.ServiceProvider.GetRequiredService<SeedInitialData>;

       // await seeDataClass.Invoke().Initial();
        await mediator.Invoke().Send(new UpdatePermissionByAssemblyCommandRequest()
        {
            IsEnable = true
        });
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
