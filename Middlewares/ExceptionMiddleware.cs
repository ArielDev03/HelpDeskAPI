using HelpDeskAPI.Exceptions;
using HelpDeskAPI.Responses;
using System.Text.Json;

namespace HelpDeskAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    //400 = No encontrado
    //401 = No autenticado
    //403 = Autenticado pero sin permisos
    //500 = Error del servidor

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
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
            _logger.LogWarning(ex, "Error de negocio: {Message}", ex.Message);

            await HandleExceptionAsync(
            context,
            StatusCodes.Status400BadRequest,
            ex.Message);
        }

        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex,"Recurso no encontrado: {Message}",ex.Message);

            await HandleExceptionAsync(
            context,
            StatusCodes.Status404NotFound,
            ex.Message);
        }
        catch (UnauthorizedException ex)
        {

            _logger.LogWarning(ex,"Acceso no autorizado: {Message}",ex.Message);

            await HandleExceptionAsync(
            context,
            StatusCodes.Status401Unauthorized,
            ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Error interno no controlado");

            await HandleExceptionAsync(
            context,
            StatusCodes.Status500InternalServerError,
            "Ocurrió un error interno");
        }
    }


}
