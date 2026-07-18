using BuildingBlocks.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class CustomeExceptionHandler(ILogger<CustomeExceptionHandler> logger) : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
  {
    logger.LogError("Error Message : {exceptionMessage}, Time of occurance {time}", exception.Message, DateTime.UtcNow);

    (string Detail, string Title, int StatusCode) details = exception switch
    {
      ValidationException =>
        (
          Detail: exception.Message,
          Title: GetType().Name,
          StatusCode: StatusCodes.Status400BadRequest
        ),

      BadHttpRequestException =>
      (
          Detail: exception.Message,
          Title: GetType().Name,
          StatusCode: StatusCodes.Status400BadRequest
      ),

      NotFoundException =>
      (
          Detail: exception.Message,
          Title: GetType().Name,
          StatusCode: StatusCodes.Status404NotFound
      ),

      InternalServerException =>
      (
          Detail: exception.Message,
          Title: GetType().Name,
          StatusCode: StatusCodes.Status500InternalServerError
      ),


      _ =>
      (
          Detail: exception.Message,
          Title: GetType().Name,
          StatusCode: StatusCodes.Status500InternalServerError
      )

    };
    var problemDetails = new ProblemDetails
    {
      Status = details.StatusCode,
      Title = details.Title,
      Detail = details.Detail,
      Instance = context.Request.Path
    };
    problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
    if (exception is  ValidationException  validationException)
    {
      problemDetails.Extensions.Add("ValidationsErros", validationException.Errors);
    }
    await context.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);
    return true;
  }
}