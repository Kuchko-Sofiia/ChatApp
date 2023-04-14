using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    public class TestController : Controller
    {
        [Authorize]
        [HttpGet]
        public string Test()
        {
            return "ok";
        }

    }
}
