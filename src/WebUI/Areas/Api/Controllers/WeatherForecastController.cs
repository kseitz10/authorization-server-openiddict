using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AuthorizationServer.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace AuthorizationServer.WebUI.Api.Controllers
{
    public class WeatherForecastController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }
    }
}
