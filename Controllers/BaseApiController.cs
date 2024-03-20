using dating_backend.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace dating_backend.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")] // Route for our controller.
    [ApiController] // Injecting features of ApiController to the Class;
    public class BaseApiController : ControllerBase
    {
    }
}
