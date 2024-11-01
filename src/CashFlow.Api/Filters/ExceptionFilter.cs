using System.Net;
using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CashFlowExeption)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(context.Exception.Message);
        
        switch (context.Exception)
        {
            case ErrorOnValidationException exception:
            {
                errorResponse = new ResponseErrorJson(exception.Errors);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;
            }
            
            default:
                context.Result = new ObjectResult(errorResponse);
                break;
        }
    }

    private static void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);
        
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}