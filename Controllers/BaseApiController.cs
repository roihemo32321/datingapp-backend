using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dating_backend.Controllers
{
    [Route("api/[controller]")] // Route for our controller.
    [ApiController] // Injecting features of ApiController to the Class;
    public class BaseApiController : ControllerBase
    {
    }
}
