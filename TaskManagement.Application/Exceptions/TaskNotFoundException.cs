
using System.Net;

namespace TaskManagement.Application.Exceptions
{
    public class TaskNotFoundException : AppException
    {
        public TaskNotFoundException() 
            :base("Task not found", (int)HttpStatusCode.NotFound)
        {}
    }
}
