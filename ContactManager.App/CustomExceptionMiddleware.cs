using ContactManager.Controllers;
using System.Net;

namespace ContactManager.App
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ContactsController> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ContactsController> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
            }
        }
      
    }
}
