namespace FootyLeague.API.Infrastructure
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using SendGrid.Helpers.Errors.Model;

    using JsonConvert = Newtonsoft.Json.JsonConvert;

    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException e)
            {
                this._logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await WriteProblemDetails(context, "Not found", "The requested resource could not be found.");
            }
            catch (ValidationException e)
            {
                this._logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await WriteProblemDetails(context, "Bad request", e.Message);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await WriteProblemDetails(context, "Server error", "An internal server error occurred.");
            }
        }

        private static async Task WriteProblemDetails(HttpContext context, string title, string detail)
        {
            ProblemDetails problem = new()
            {
                Status = context.Response.StatusCode,
                Title = title,
                Detail = detail
            };

            string json = JsonConvert.SerializeObject(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}