using Microsoft.AspNetCore.Mvc;
using MP.MKKing.API.Errors;

namespace MP.MKKing.API.Controllers
{
    // Ignore as API Endpoint
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("errors/{code}")]
    public class ErrorController : BaseApiController
    {
        // Handle any HTTP Method
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}