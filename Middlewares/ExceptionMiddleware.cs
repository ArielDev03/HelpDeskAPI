using HelpDeskAPI.Exceptions;
using HelpDeskAPI.Responses;
using System.Text.Json;

namespace HelpDeskAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    //401 = No autenticado
    //403 = Autenticado pero sin permisos
    //500 = Error del servidor

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private async Task HandleExceptionAsync(
    HttpContext context,
    int statusCode,
    string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            StatusCode = statusCode,
            Message = message
        };

        var result = JsonSerializer.Serialize(errorResponse);


        await context.Response.WriteAsync(result);
    }


    public async Task InvokeAsync(HttpContext context)
    {

        try
        {
            await _next(context);
        }
        catch (BusinessException ex)
        {
            
            await HandleExceptionAsync(
            context,
            StatusCodes.Status400BadRequest,
            ex.Message);
        }

        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(
            context,
            StatusCodes.Status404NotFound,
            ex.Message);
        }
        catch (UnauthorizedException ex)
        {
            await HandleExceptionAsync(
            context,
            StatusCodes.Status401Unauthorized,
            ex.Message);
        }
        catch (Exception)
        {
            await HandleExceptionAsync(
            context,
            StatusCodes.Status500InternalServerError,
            "Ocurrió un error interno");
        }
    }


}
