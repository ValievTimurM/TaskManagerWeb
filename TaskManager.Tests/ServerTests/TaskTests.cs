using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces.Services.Ref;
using TaskManager.Application.Models.Common;
using TaskManager.Application.Models.ViewModels;
using TaskManagerWeb.Server.Controllers;

namespace TaskManager.Tests.ServerTests
{
  public class TaskTests
  {
    [SetUp]
    public void Setup()
    {


    }

    [Test]
    public async Task TaskAdd_ResultFail_BadRequest()
    {
      var modelToAdd = new TaskViewModel();
      ITaskService taskService = Substitute.For<ITaskService>();
      ICommentService commentMock = Substitute.For<ICommentService>();
      var _controller = new TaskController(taskService, commentMock);

      taskService.Add(Arg.Any<TaskViewModel>()).Returns(ServiceResult.Fail("Ошибка"));

      var result = await _controller.Add(modelToAdd);


      Assert.NotNull(result);
      Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task TaskAdd_ResultOK_OKResult()
    {
      var modelToAdd = new TaskViewModel();
      ITaskService taskService = Substitute.For<ITaskService>();
      ICommentService commentMock = Substitute.For<ICommentService>();
      var _controller = new TaskController(taskService, commentMock);

      taskService.Add(Arg.Any<TaskViewModel>()).Returns(ServiceResult.Ok());

      var result = await _controller.Add(modelToAdd);


      Assert.NotNull(result);
      Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public async Task TaskUpdate_ResultFail_BadRequest()
    {
      var modelToAdd = new TaskViewModel();
      ITaskService taskService = Substitute.For<ITaskService>();
      ICommentService commentMock = Substitute.For<ICommentService>();
      var _controller = new TaskController(taskService, commentMock);

      taskService.Update(Arg.Any<TaskViewModel>()).Returns(ServiceResult.Fail("Ошибка"));

      var result = await _controller.Update(modelToAdd);


      Assert.NotNull(result);
      Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task TaskUpdate_ResultOK_OKResult()
    {
      var modelToAdd = new TaskViewModel();
      ITaskService taskService = Substitute.For<ITaskService>();
      ICommentService commentMock = Substitute.For<ICommentService>();
      var _controller = new TaskController(taskService, commentMock);

      taskService.Update(Arg.Any<TaskViewModel>()).Returns(ServiceResult.Ok());

      var result = await _controller.Update(modelToAdd);


      Assert.NotNull(result);
      Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public async Task TaskDelete_ResultFail_BadRequest()
    {
      var modelToAdd = new TaskViewModel();
      ITaskService taskService = Substitute.For<ITaskService>();
      ICommentService commentMock = Substitute.For<ICommentService>();
      var _controller = new TaskController(taskService, commentMock);

      taskService.Delete(Arg.Any<Guid>()).Returns(ServiceResult.Fail("Ошибка"));

      var result = await _controller.Delete(modelToAdd);


      Assert.NotNull(result);
      Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task TaskDelete_ResultOK_OKResult()
    {
      var modelToAdd = new TaskViewModel();
      ITaskService taskService = Substitute.For<ITaskService>();
      ICommentService commentMock = Substitute.For<ICommentService>();
      var _controller = new TaskController(taskService, commentMock);

      taskService.Delete(Arg.Any<Guid>()).Returns(ServiceResult.Ok());

      var result = await _controller.Delete(modelToAdd);


      Assert.NotNull(result);
      Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public async Task TaskGet_OKResult()
    {
      var models = new List<TaskViewModel>() { TaskViewModel.New(creator: "testCreator"), TaskViewModel.New(creator: "testCreator2") };
      ITaskService taskService = Substitute.For<ITaskService>();
      ICommentService commentMock = Substitute.For<ICommentService>();
      var _controller = new TaskController(taskService, commentMock);

      taskService.Get().Returns(models);

      IActionResult result = await _controller.Get();

      var okObjectResult = result as OkObjectResult;
      Assert.IsNotNull(okObjectResult);
      var entities = okObjectResult.Value as IEnumerable<TaskViewModel>;
      Assert.IsNotNull(entities);
      Assert.NotNull(entities);
      Assert.That(entities.Count, Is.EqualTo(2));
      Assert.That(entities.Any(p => p.Creator == "testCreator"), Is.EqualTo(true));
    }

    [Test]
    public async Task TaskGetBy_OKResult()
    {
      var model = TaskViewModel.New(creator: "testCreator");
      ITaskService taskService = Substitute.For<ITaskService>();
      ICommentService commentMock = Substitute.For<ICommentService>();
      var _controller = new TaskController(taskService, commentMock);

      taskService.GetBy(Arg.Any<Guid>()).Returns(model);

      IActionResult result = await _controller.Getby(Guid.NewGuid());

      var okObjectResult = result as OkObjectResult;
      Assert.IsNotNull(okObjectResult);
      var entitiy = okObjectResult.Value as TaskViewModel;
      Assert.IsNotNull(entitiy);
      Assert.NotNull(entitiy);
      Assert.That(entitiy.Creator, Is.EqualTo("testCreator"));
    }
  }
}
