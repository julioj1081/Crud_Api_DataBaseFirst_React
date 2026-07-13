using ApiCoreID.Services;

namespace ApiCoreID.Middleware
{
    public class EjMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<EjMiddleware> _logger;

        public EjMiddleware(RequestDelegate next, ILogger<EjMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, GuidServices guidServices)
        {
            var logMessage = $"Middleware {guidServices.resultadoGuid}";
            _logger.LogInformation(logMessage);

            await _next(context); 
        }
    }
}