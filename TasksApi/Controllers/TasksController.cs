using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TasksApi.Models;
using TasksApi.Repositories;

namespace TasksApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(ITaskRepository repository) : ControllerBase
{
    [HttpGet]
    public IEnumerable<TaskItem> GetAll() => repository.GetAll();

    [HttpGet("{id:int}")]
    public ActionResult<TaskItem> GetById(int id)
    {
        var task = repository.GetById(id);
        return task is null ? NotFound() : task;
    }

    [HttpPost]
    public ActionResult<TaskItem> Create(CreateTaskRequest request)
    {
        var task = repository.Add(request.Title);

        if (task.Id == 0)
        {
            return Problem(
            detail: "Failed to save the task.",
            statusCode: StatusCodes.Status500InternalServerError
            );
        }

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }
}

public record CreateTaskRequest([Required] string Title);
