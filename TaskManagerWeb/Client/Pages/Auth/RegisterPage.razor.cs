using Microsoft.AspNetCore.Components;
using TaskManager.Application.Models.Auth;
using TaskManagerWeb.Client.AuthProvider;

namespace TaskManagerWeb.Client.Pages.Auth
{
    public partial class RegisterPage
    {
        [Inject]
        private CustomStateProvider _stateProvider { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private RegisterModel registerModel { get; set; } = RegisterModel.New();
        private string? error { get; set; }

        private async Task Register()
        {
            error = null;
            try
            {
                await _stateProvider.Register(registerModel);
                _navigationManager.NavigateTo("");
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
        }
    }
}
