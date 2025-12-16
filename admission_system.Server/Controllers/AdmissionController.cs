using admission_system.Server.Models;
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

        public AdmissionController(IAdmissionService admissionService)
        {
            _admissionService = admissionService;
        }

        [HttpPost("formCheck")]
        public IActionResult FormCheck([FromBody] VisitorRequest visitorRequest)
        {
            return Ok(_admissionService.CheckVisitor(visitorRequest));
        }
    }
}
