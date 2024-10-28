using Microsoft.AspNetCore.Components;
using TaskManager.Application.Models.Auth;
using TaskManagerWeb.Client.AuthProvider;

namespace TaskManagerWeb.Client.Pages.Auth
{
  public partial class LoginPage
  {
    [Inject]
    private CustomStateProvider _stateProvider { get; set; }
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    private LoginModel _loginModel { get; set; } = LoginModel.New();
    private string? error { get; set; }

    private async Task Login()
    {
      error = null;
      try
      {
        await _stateProvider.Login(_loginModel);
        _navigationManager.NavigateTo("");
      }
      catch (Exception ex)
      {
        error = ex.Message;
      }
    }
  }
}
