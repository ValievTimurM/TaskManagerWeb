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
  public class CommentControllerTest
  {
    [SetUp]
    public void Setup()
    {


    }

    [Test]
    public async Task CommentAdd_ResultFail_BadRequest()
    {
      var modelToAdd = new CommentViewModel();
      ICommentService commentMock = Substitute.For<ICommentService>();
      var _controller = new CommentController(commentMock);

      commentMock.Add(Arg.Any<CommentViewModel>()).Returns(ServiceResult.Fail("Ошибка"));

      var result = await _controller.Add(modelToAdd);


      Assert.NotNull(result);
      Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task CommentAdd_ResultOK_OKResult()
    {
      ICommentService commentMock = Substitute.For<ICommentService>();
      var _controller = new CommentController(commentMock);
      var modelToAdd = new CommentViewModel();
      commentMock.Add(modelToAdd).Returns(ServiceResult.Ok());

      var result = await _controller.Add(modelToAdd);


      Assert.NotNull(result);
      Assert.That(result, Is.InstanceOf<OkResult>());
    }
  }
}
