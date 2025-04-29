using Microsoft.AspNetCore.Mvc;

namespace MoviesApi.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult CustomResponse(object? responseObject = null, bool isSuccess = false, string returnMessage = "")
        {
            if(responseObject != null)
            {
                if (isSuccess)
                {
                    return Ok(new
                    {
                        success = isSuccess,
                        data = responseObject
                    });
                }

                return BadRequest(new
                {
                    success = isSuccess,
                    message = returnMessage
                });
            }

            return NotFound(new
            {
                succes = isSuccess,
                message = returnMessage
            });
        }

        protected IActionResult PostCustomResponse(object? responseObject = null, bool isSuccess = false, string returnMessage = "")
        {
            if (isSuccess && responseObject is null)
                return Created();

            if (isSuccess)
            {
                return Ok(new
                {
                    success = isSuccess,
                    message = returnMessage,
                    data = responseObject
                });
            }

            return BadRequest(new
            {
                success = isSuccess,
                message = returnMessage
            });
        }
    }
}
