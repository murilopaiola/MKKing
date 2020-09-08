using Microsoft.AspNetCore.Mvc;
using MP.MKKing.API.Errors;

namespace MP.MKKing.API.Controllers
{
    [Route("errors/{code}")]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}