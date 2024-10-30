using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TaskManager.Application.Interfaces.Services.Ref;
using TaskManager.Application.Mapping;
using TaskManager.Application.Models.ViewModels;
using TaskManager.Core.Models;

namespace TaskManagerWeb.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class TaskController : ControllerBase
  {
    private readonly ITaskService _tasksService;
    private readonly ICommentService _comService;
    public TaskController(ITaskService taskService, ICommentService comService)
    {
      _tasksService = taskService;
      _comService = comService;
    }

    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> Get()
    {
      IList<TaskViewModel> tasks = await _tasksService.Get();
      return Ok(tasks);
    }

    //[HttpGet("{id}")]
    [HttpGet]
    [Route("getby/{id}")]
    public async Task<IActionResult> Getby(Guid id)
    {
      var task = await _tasksService.GetBy(id);
      return Ok(task);
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add(TaskViewModel modelToAdd)
    {
      var result = await _tasksService.Add(modelToAdd);

      if (result.Success)
      {
        return Ok();
      }
      else
      {
        return BadRequest(result.Message);
      }
    }

    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> Update(TaskViewModel modelToUpdate)
    {
      var result = await _tasksService.Update(modelToUpdate);
      if (result.Success)
      {
        return Ok();
      }
      else
      {
        return BadRequest(result.Message);
      }
    }

    [HttpPost]
    [Route("Delete")]
    public async Task<IActionResult> Delete(TaskViewModel modelToDelete)
    {
      var result = await _tasksService.Delete(modelToDelete.Id);

      if (result.Success)
      {
        return Ok();
      }
      else
      {
        return BadRequest(result.Message);
      }
    }
  }
}
