namespace HelpDeskAPI.Exceptions
{
    //Convierte errores a respuestas HTTP
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message)
           : base(message)
        {
        }
    }
}
