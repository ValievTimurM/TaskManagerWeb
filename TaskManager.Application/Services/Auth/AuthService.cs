using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces.Services.Auth;
using TaskManager.Application.Models.Auth;

namespace TaskManager.Application.Services.Auth
{
  public class AuthService : IAuthService
    {
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<CurrentUser> CurrentUserInfo()
    {
      var result = await _httpClient.GetFromJsonAsync<CurrentUser>("api/auth/currentuserinfo");
      return result;
    }

    public async Task Login(LoginModel loginRequest)
    {
      var result = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
      if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
      result.EnsureSuccessStatusCode();
    }

    public async Task Logout()
    {
      var result = await _httpClient.PostAsync("api/auth/logout", null);
      result.EnsureSuccessStatusCode();
    }

    public async Task Register(RegisterModel registerRequest)
    {
      var result = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequest);
      if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
      result.EnsureSuccessStatusCode();
    }
  }
}
