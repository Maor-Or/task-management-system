using System.Net;

namespace TaskManagement.Application.Exceptions
{
    public class UserAlreadyExistsException : AppException
    {
        public UserAlreadyExistsException(string email)
            : base($"User with email '{email}' already exists", (int)HttpStatusCode.Conflict)
        {
        }
    }
}
