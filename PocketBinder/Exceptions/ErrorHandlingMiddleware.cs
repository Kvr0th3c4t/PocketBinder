namespace PocketBinder.Exceptions
{
    public class ErrorHandlingMiddleware
    {
        // Middleware personalizado para manejar excepciones y devolver respuestas JSON con el código de estado y el mensaje de error
        private readonly RequestDelegate _next;

        // El constructor recibe el siguiente middleware en la cadena de ejecución
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // El método InvokeAsync se encarga de ejecutar el siguiente middleware y capturar cualquier excepción que ocurra
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        // El método HandleExceptionAsync se encarga de determinar el código de estado HTTP adecuado según el tipo de excepción y devolver una respuesta JSON con el mensaje de error
        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Determinar el código de estado HTTP según el tipo de excepción
            var statusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ForbiddenException => StatusCodes.Status403Forbidden,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            // Establecer el código de estado HTTP y el tipo de contenido de la respuesta
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            // Escribir la respuesta JSON con el código de estado y el mensaje de error
            await context.Response.WriteAsJsonAsync(new
            {
                status = statusCode,
                message = ex.Message
            });
        }
    }
}
