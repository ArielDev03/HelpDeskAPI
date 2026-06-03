namespace HelpDeskAPI.Exceptions
{
    //Representa un error de negocio
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        {
        }
    }
}
