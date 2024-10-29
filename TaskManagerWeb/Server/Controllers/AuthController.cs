using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Models.Auth;
using TaskManager.Infrastructure.Auth;

namespace TaskManagerWeb.Server.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel request)
    {
      var user = await _userManager.FindByNameAsync(request.UserName);
      if (user == null)
        return BadRequest("Пользователя не существует!");

      var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
      if (!result.Succeeded)
        return BadRequest("Неправильный пароль!");

      await _signInManager.SignInAsync(user, request.RememberMe);
      return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel item)
    {
      var user = new AppUser();
      user.UserName = item.UserName;
      user.Name = item.Name;

      var result = await _userManager.CreateAsync(user, item.Password);
      if (!result.Succeeded)
        return BadRequest(result.Errors.FirstOrDefault()?.Description);

      return await Login(LoginModel.New(item.UserName, item.Password));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return Ok();
    }

    [HttpGet]
    public CurrentUser CurrentUserInfo()
    {
      var currentUser = CurrentUser.New(User?.Identity?.IsAuthenticated ?? false,
                                        User?.Identity?.Name,
                                        User?.Claims.ToDictionary(c => c.Type, c => c.Value));
      return currentUser;
    }
  }
}
