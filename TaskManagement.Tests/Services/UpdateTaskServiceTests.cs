using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;
using Xunit;

namespace TaskManagement.Tests.Services
{
    public class UpdateTaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<ILogger<UpdateTaskService>> _loggerMock;
        private readonly UpdateTaskService _service;

        public UpdateTaskServiceTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _loggerMock = new Mock<ILogger<UpdateTaskService>>();

            _service = new UpdateTaskService(_taskRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task UpdateAsync_ShouldthrowException_WhenUserDoesNotOwnTask() 
        {
            // Arrange

            var taskId = Guid.NewGuid();
            var ownerId = Guid.NewGuid();
            var otherUserId = Guid.NewGuid();

            var task = new TaskItem
            {
                Id = taskId,
                UserId = ownerId
            };

            _taskRepositoryMock.Setup(r => r.GetByIdAsync(taskId))
                .ReturnsAsync(task);

            var dto = new UpdateTaskDto
            {
                Title = "Test",
                Description = "Test",
                DueDate = DateTime.UtcNow,
                Priority = 1
            };

            //Act + assert

            await Assert.ThrowsAsync<ForbiddenTaskAccessException>(
                () => _service.UpdateAsync(
                    taskId,
                    otherUserId,
                    dto));
        }
    }
}
