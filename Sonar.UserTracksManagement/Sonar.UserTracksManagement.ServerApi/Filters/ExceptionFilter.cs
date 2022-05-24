using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sonar.UserTracksManagement.Application.Tools;

namespace ServerApi.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new JsonResult(context.Exception.Message)
        {
            StatusCode = context.Exception switch
            {
                InvalidArgumentsException => (int) HttpStatusCode.BadRequest,
                NotFoundArgumentsException => (int) HttpStatusCode.NotFound,
                UserAccessException => (int) HttpStatusCode.Forbidden,
                UserAuthorizationException => (int) HttpStatusCode.Unauthorized,
                _ => (int) HttpStatusCode.InternalServerError
            }
        };
        context.ExceptionHandled = true;
    }
}