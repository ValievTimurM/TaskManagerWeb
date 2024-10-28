﻿using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using TaskManager.Application.Interfaces.Services.Auth;
using TaskManager.Application.Models.Auth;

namespace TaskManagerWeb.Client.AuthProvider
{
  public class CustomStateProvider : AuthenticationStateProvider
  {
    private readonly IAuthService _authService;
    private CurrentUser _currentUser;
    public CustomStateProvider(IAuthService authService)
    {
      _authService = authService;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      var identity = new ClaimsIdentity();
      try
      {
        var userInfo = await GetCurrentUser();
        if (userInfo.IsAuthenticated)
        {
          var claims = new[] { new Claim(ClaimTypes.Name, _currentUser.UserName) }.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
          identity = new ClaimsIdentity(claims, "Server authentication");
        }
      }
      catch (HttpRequestException ex)
      {
        Console.WriteLine("Request failed:" + ex.ToString());
      }

      return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private async Task<CurrentUser> GetCurrentUser()
    {
      if (_currentUser != null && _currentUser.IsAuthenticated) return _currentUser;
      _currentUser = await _authService.CurrentUserInfo();
      return _currentUser;
    }
    public async Task Logout()
    {
      await _authService.Logout();
      _currentUser = null;
      NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    public async Task Login(LoginModel loginParameters)
    {
      await _authService.Login(loginParameters);
      NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    public async Task Register(RegisterModel registerParameters)
    {
      await _authService.Register(registerParameters);
      NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
  }
}
