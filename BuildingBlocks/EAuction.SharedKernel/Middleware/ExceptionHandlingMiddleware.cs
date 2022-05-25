using Auction.SharedKernel.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Auction.SharedKernel.Middleware
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ApiException ex)
            {
                logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
            catch (ValidationException ex)
            {
                logger.LogError(ex, ex.Message);
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                throw;
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, ApiException exception)
        {
            var response = new
            {
                title = exception.Message,
                status = exception.Status,
                details = exception.Details,
                errors = GetErrors(exception)
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = exception.Status;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static async Task HandleValidationExceptionAsync(HttpContext httpContext, ValidationException exception)
        {
            var response = new
            {
                title = exception.Errors.FirstOrDefault().ErrorMessage,
                status = StatusCodes.Status400BadRequest,
                details = exception.Data,
                errors = GetErrors(exception)
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors.GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);
            }
            return errors;
        }
    }
}
