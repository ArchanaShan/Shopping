using Microsoft.AspNetCore.Mvc;

namespace Shopping.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    ViewBag.ErrorMessage = "Bad Request: The request could not be understood or was missing required parameters.";
                    break;
                case 401:
                    ViewBag.ErrorMessage = "Unauthorized: You do not have permission to access this resource.";
                    break;
                case 403:
                    ViewBag.ErrorMessage = "Forbidden: You do not have permission to view this page.";
                    break;
                case 404:
                    ViewBag.ErrorMessage = "Not Found: The resource you requested could not be found.";
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Internal Server Error: An unexpected error occurred on the server.";
                    break;
                default:
                    ViewBag.ErrorMessage = "An error occurred while processing your request.";
                    break;
            }

            return View("NotFound");
        }
    }
}
