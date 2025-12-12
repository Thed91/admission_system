using admission_system.Server.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace admission_system.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionService _admissionService;
   
        [HttpPost("formCheck")]
        public IActionResult FormCheck([FromBody] string value)
        {
            return Ok(value);
        }
    }
}
