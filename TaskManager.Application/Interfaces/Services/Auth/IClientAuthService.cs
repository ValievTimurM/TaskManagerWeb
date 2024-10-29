using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Models.Auth;

namespace TaskManager.Application.Interfaces.Services.Auth
{
  public interface IClientAuthService
  {
    Task Login(LoginModel loginRequest);
    Task Register(RegisterModel registerRequest);
    Task Logout();
    Task<CurrentUser> CurrentUserInfo();
  }
}
