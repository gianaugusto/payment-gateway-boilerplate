namespace PaymentGateway.Api.Middleware
{
    using System.Net;

    using Newtonsoft.Json;

    using PaymentGateway.Application.Core.Model;
    using PaymentGateway.CrossCutting.Exceptions;

    /// <summary>
    /// 
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly bool _isDevelopment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="isDevelopment"></param>
        public ErrorHandlingMiddleware(RequestDelegate next, bool isDevelopment)
        {
            _next = next;
            _isDevelopment = isDevelopment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (
                    context.Response.StatusCode is 401 or 403)
                {
                    await RewriteResponseAsync(
                        context,
                        context.Response.StatusCode == 401 ? "Unauthorized" : "Request has been denied",
                        context.Response.StatusCode,
                        string.Empty);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var exceptionType = exception.GetType();
            HttpStatusCode status;
            if (exceptionType == typeof(BadRequestException))
            {
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                status = HttpStatusCode.NotFound;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
            }

            return RewriteResponseAsync(context, exception.Message, (int)status, exception.StackTrace);
        }

        private Task RewriteResponseAsync(HttpContext context, string exceptionMessage, int status,
            string stackTrace)
        {
            var result = JsonConvert.SerializeObject(new
            {
                Code = context.Response.StatusCode,
                Message = exceptionMessage,

                ApplicationError = _isDevelopment ? new ApplicationError()
                {
                    DeveloperMessage = stackTrace,
                    Code = status,
                } : null
            }, Formatting.Indented);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;
            return context.Response.WriteAsync(result);
        }
    }
}
