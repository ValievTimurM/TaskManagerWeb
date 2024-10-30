using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TaskManager.Application.Interfaces.Services.Ref;
using TaskManager.Application.Mapping;
using TaskManager.Application.Models.ViewModels;
using TaskManager.Application.Services.Ref;
using TaskManager.Core.Models;

namespace TaskManagerWeb.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class CommentController : ControllerBase
  {
    private readonly ICommentService _commentService;
    public CommentController(ICommentService commentService)
    {
      _commentService = commentService;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add(CommentViewModel modelToAdd)
    {
      var result = await _commentService.Add(modelToAdd);

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
