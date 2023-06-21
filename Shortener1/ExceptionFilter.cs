using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Shortener1;

public class ExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is ArgumentNullException)
        {
            context.Result = new BadRequestObjectResult("Invalid request");
            context.ExceptionHandled = true;
            return Task.CompletedTask;
            
        }
        if (context.Exception is DbUpdateException )
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            context.ExceptionHandled = true;
            
            return Task.CompletedTask;
        }
        if (context.Exception is NullReferenceException )
        {
            context.Result = new BadRequestObjectResult(ResponseMessages.FailedToFindLongUrlFromShortened+ context.Exception.Message);
            
            context.ExceptionHandled = true;
            
            return Task.CompletedTask;
        }
        return Task.FromException(context.Exception);
    }
    
}