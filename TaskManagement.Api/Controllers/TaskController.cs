using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Application.Common;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces.Services;
using TaskManagement.Application.Services;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly CreateTaskService _createTaskService;
    private readonly GetUserTasksService _getUserTasksService;
    private readonly CompleteTaskService _completeTaskService;
    private readonly DeleteTaskService _deleteTaskService;
    private readonly UpdateTaskService _updateTaskService;
    private readonly ICurrentUserService _currentUserService;

    public TaskController(CreateTaskService createTaskService, 
        GetUserTasksService getUserTasksService,
        CompleteTaskService completeTaskService,
        DeleteTaskService deleteTaskService,
        UpdateTaskService updateTaskService,
        ICurrentUserService currentUserService)
    {
        _createTaskService = createTaskService;
        _getUserTasksService = getUserTasksService;
        _completeTaskService = completeTaskService;
        _deleteTaskService = deleteTaskService;
        _updateTaskService = updateTaskService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        var userId = _currentUserService.GetUserId();

        var result = await _createTaskService.CreateAsync(dto, userId);

        return Ok(new ApiResponse<TaskDto> (true, "Task created successfully", result));
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyTasks(
        int page = 1,
        int pageSize = 10)
    {
        var userId = _currentUserService.GetUserId();

        var result = await _getUserTasksService.GetPagedAsync(userId, page, pageSize);

        return Ok(
            new ApiResponse<PagedResult<TaskDto>>(
                true,
                "Tasks retrieved successfully",
                result
        ));
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        var userId = _currentUserService.GetUserId();

        await _completeTaskService.CompleteAsync(id, userId);
        return Ok(
            new ApiResponse<object>(
                true,
                "Task marked as completed"
            )
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = _currentUserService.GetUserId();
        await _deleteTaskService.DeleteAsync(id, userId);

        return Ok(
            new ApiResponse<object>(
                true,
                "Task deleted successfully"
                )
        );
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateTaskDto dto)
    {
        var userId = _currentUserService.GetUserId();

        await _updateTaskService.UpdateAsync(id, userId, dto);
        
        return Ok(
            new ApiResponse<object>(
                true,
                "Task updated successfully"
                )
        );
    }
}