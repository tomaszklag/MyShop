using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyShop.Core.Domain;
using MyShop.Core.Domain.Exceptions;
using Newtonsoft.Json;

namespace MyShop.Infrastructure.Mvc
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
            
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "There was occurred an error.";

            switch(exception)
            {
                case MyShopException e:
                    errorCode = e.Code;
                    message = e.Message;
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                
                case NotFoundException e:
                    errorCode = e.Code;
                    message = e.Message;
                    statusCode = HttpStatusCode.NotFound;
                    break;
            }

            var response = new { code = errorCode, message = exception.Message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}