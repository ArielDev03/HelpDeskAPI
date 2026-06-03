namespace HelpDeskAPI.Exceptions
{

    //Representa que algo no existe
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
