using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Web.Common.ExceptionModels;
using Web.LoggerService;
using Web.Service.DtoModels;
using WebAPI.Models;

namespace WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
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
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var exType = exception.GetType();
            ErrorDetails err = new ErrorDetails();
            switch (exception)
            {
                case Exception e when exType == typeof(NotFoundException):
                    err.StatusCode = (int)HttpStatusCode.NotFound;
                    err.Message = exception.Message;
                    _logger.LogError($"Something went wrong: {exception.StackTrace}");
                    break;

                default:
                    err.StatusCode = (int)HttpStatusCode.InternalServerError;
                    err.Message = "Internal Server Error";
                    break;
            }
            return context.Response.WriteAsync(err.ToString());
        }
    }
}
