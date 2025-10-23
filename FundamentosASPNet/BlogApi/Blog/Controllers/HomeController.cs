using Blog.Atributes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")] // raiz da raiz Health Check
        //[ApiKey]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
