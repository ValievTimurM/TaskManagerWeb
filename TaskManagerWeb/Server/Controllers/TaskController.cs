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
    private readonly ITaskService _taskService;
    public TaskController(ITaskService taskService)
    {
      _taskService = taskService;
    }

    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> Get()
    {
      IList<TaskViewModel> tasks = await _taskService.Get();
      return Ok(tasks);
    }

    //[HttpGet("{id}")]
    [HttpGet]
    [Route("getby/{id}")]
    public async Task<IActionResult> Getby(Guid id)
    {
      var task = await _taskService.GetBy(id);
      return Ok(task);
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add(TaskViewModel modelToAdd)
    {
      var result = await _taskService.Add(modelToAdd);

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
      var result = await _taskService.Update(modelToUpdate);
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
      var result = await _taskService.Delete(modelToDelete.Id);

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
