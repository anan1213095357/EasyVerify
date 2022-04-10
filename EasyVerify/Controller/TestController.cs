using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyVerify.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [IdentityAuthentication(UserLevel.Administrators)]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "123";
        }
    }
}
