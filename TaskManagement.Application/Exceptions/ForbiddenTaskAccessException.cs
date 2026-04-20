
using System.Net;
using System.Runtime.InteropServices.Marshalling;

namespace TaskManagement.Application.Exceptions
{
    public class ForbiddenTaskAccessException : AppException
    {
        public ForbiddenTaskAccessException()
            :base("You are not allowed to access this task",(int)HttpStatusCode.Forbidden)
            { }
    }
}
