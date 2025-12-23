using Microsoft.AspNetCore.Mvc;

namespace ChineseAction.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DonorController : ControllerBase
    {
       
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

           
        }
    }
}
