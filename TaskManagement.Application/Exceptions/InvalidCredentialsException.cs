using System.Net;
namespace TaskManagement.Application.Exceptions
{
    public class InvalidCredentialsException : AppException
    {

        public InvalidCredentialsException()
            :base("invalid email or password",(int)HttpStatusCode.Unauthorized)
        {
        }
    }
}
