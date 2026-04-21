using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;
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

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenTaskDoesNotExist()
        {
            //Arrange

            var taskId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _taskRepositoryMock
                .Setup(r => r.GetByIdAsync(taskId))
                .ReturnsAsync((TaskItem?)null);

            var dto = new UpdateTaskDto
            {
                Title = "Test",
                Description = "Test",
                DueDate = DateTime.UtcNow,
                Priority = 1
            };

            // Act + assert

            await Assert.ThrowsAsync<TaskNotFoundException>(
                () => _service.UpdateAsync(
                    taskId,
                    userId,
                    dto));
        }
    
        [Fact]
        public async Task UpdateAsync_ShouldUpdateTask_WhenUserOwnsTask()
        {
            //Arrange 
            var taskId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var task = new TaskItem
            {
                Id = taskId,
                UserId = userId,
                Title = "Old Title"
            };

            _taskRepositoryMock
                .Setup(r => r.GetByIdAsync(taskId))
                .ReturnsAsync(task);

            var dto = new UpdateTaskDto
            {
                Title = "Updated Title",
                Description = "Updated description",
                DueDate = DateTime.UtcNow,
                Priority = 2
            };

            // Act

            await _service.UpdateAsync(taskId, userId, dto);

            // Assert

            _taskRepositoryMock
                .Verify(r => r.UpdateAsync(task), Times.Once);
            Assert.Equal("Updated Title", task.Title);
        }
    }
}
