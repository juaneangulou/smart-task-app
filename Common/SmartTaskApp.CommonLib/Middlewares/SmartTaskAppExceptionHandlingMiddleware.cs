using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;

namespace SmartTaskApp.CommonLib.Middlewares
{
    public class SmartTaskAppExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public SmartTaskAppExceptionHandlingMiddleware(RequestDelegate next)
        {
            try
            {
                _next = next;
            }catch(Exception ex)
            {
                Console.WriteLine("sasa");
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            if (exception is ArgumentException) code = HttpStatusCode.BadRequest;
            if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
            if (exception is NotImplementedException) code = HttpStatusCode.NotImplemented;

            Log.Error(exception, "An error occurred while processing the request");

            var result = new ErrorResponse
            {
                SubjectError = code.ToString(),
                ErrorDescription = exception.Message
            };

            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(result);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(jsonResponse);
        }

    }

    public class ErrorResponse
    {
        public string SubjectError { get; set; }
        public string ErrorDescription { get; set; }
    }
}
