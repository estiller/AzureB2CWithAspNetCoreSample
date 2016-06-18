using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TestController
    {
        [HttpGet]
        public string Get()
        {
            return "You are secured!";
        }
    }
}