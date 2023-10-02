
using AccessManagement.Services.Permission.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAM_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
   
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get([FromServices] IEnumerable<EndpointDataSource> endpointSources)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost]
    public async Task<IActionResult> GetAll([FromServices]IMediator mediator)
    {
        try
        {
            await mediator.Send(new UpdatePermissionByAssemblyCommandRequest());
        }
        catch (Exception)
        {

            
        }
        
        return Ok();
        
    }
    public record GetControllerInfoForPermission
    {
        public string ControllerName { get; set; }  
        public string ControllerNameSpace { get; set; }
        public string ControllerActions { get; set; }
        public string ActionRequest { get; set; }
        public string ActionRoute { get; set; }
        public string ControllerRoute { get; set;}
        public string ActionName { get; set;}
    }
}
