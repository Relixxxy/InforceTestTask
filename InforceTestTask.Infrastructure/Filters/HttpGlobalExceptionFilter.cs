using System.Net;
using InforceTestTask.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace InforceTestTask.Infrastructure.Filters;

public class HttpGlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<HttpGlobalExceptionFilter> _logger;

    public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BusinessException ex)
        {
            var routeData = new RouteValueDictionary
            {
                { "controller", "Home" },
                { "action", "Error" }
            };

            var result = new RedirectToRouteResult(routeData);

            result.RouteValues.Add("status", ex.Status);
            result.RouteValues.Add("message", ex.Message);

            context.Result = result;
            context.ExceptionHandled = true;
        }
        else
        {
            _logger.LogError(
                new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);
        }
    }
}
