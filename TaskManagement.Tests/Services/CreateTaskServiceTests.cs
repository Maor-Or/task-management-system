using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

using TaskManagement.Application.Services;
using TaskManagement.Application.Interfaces.Repositories;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Tests.Services
{
    public class CreateTaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<ILogger<CreateTaskService>> _loggerMock;

        private readonly CreateTaskService _service;

        public CreateTaskServiceTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _loggerMock = new Mock<ILogger<CreateTaskService>>();
            _service = new CreateTaskService(_taskRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateTask_withCorrectUserId()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var dto = new CreateTaskDto
            {
                Title = "Test task",
                Description = "Test description",
                DueDate = DateTime.UtcNow,
                Priority = 1
            };

            TaskItem? capturedTask = null;
            _taskRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
                .Callback<TaskItem>(task =>
                {
                    capturedTask = task;
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateAsync(dto, userId);

            // Assert

            _taskRepositoryMock.Verify(r => r.AddAsync(It.IsAny<TaskItem>()),
                Times.Once);

            Assert.NotNull(capturedTask);

            Assert.Equal(userId, capturedTask!.UserId);

            Assert.Equal(dto.Title,capturedTask.Title);

            Assert.Equal(dto.Description, capturedTask.Description);
            
            Assert.Equal(dto.Priority, (int)capturedTask.Priority);

            Assert.Equal(dto.Title, result.Title);
        }
    }
}
