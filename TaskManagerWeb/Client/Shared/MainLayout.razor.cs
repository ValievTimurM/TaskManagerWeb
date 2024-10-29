using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection.Metadata;
using TaskManagerWeb.Client.AuthProvider;

namespace TaskManagerWeb.Client.Shared
{
  public partial class MainLayout
  {
    [Inject]
    private CustomStateProvider _stateProvider { get; set; }
    [Inject]
    private NavigationManager _navigationManager { get; set; }

    [CascadingParameter]
    Task<AuthenticationState> _authenticationState { get; set; }

    private string? _login;

    protected override async Task OnParametersSetAsync()
    {
      if (!(await _authenticationState).User.Identity.IsAuthenticated)
      {
        _navigationManager.NavigateTo("/login");
      }
      _login = (await _authenticationState).User.Identity.Name;
    }
    async Task LogoutClick()
    {
      await _stateProvider.Logout();
      _navigationManager.NavigateTo("/login");
    }
  }
}
