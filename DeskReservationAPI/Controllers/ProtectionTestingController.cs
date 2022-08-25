using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeskReservationAPI.Controllers
{
    [Authorize(Policy = "TokenPolicy", Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProtectionTestingController : ControllerBase
    {
        [HttpGet]
        public string Test()
        {
            return "test token";
        }
    }
}
