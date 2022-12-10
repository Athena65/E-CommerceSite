using ClientSide.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Components;

namespace ClientSide.Pages
{
    public partial class Login
    {
        private UserForAuthenticationDto _userForAuth=new UserForAuthenticationDto();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager Navigate { get; set; }
        public bool ShowAuthError { get; set; }
        public string? Error { get; set; }
        public async Task ExecuteLogin()
        {
            ShowAuthError= false;
            var result = await AuthenticationService.Login(_userForAuth);
            if(!result.IsAuthSuccessful)
            {
                Error= result.ErrorMessage;
                ShowAuthError= true;
            }
            else
            {
                Navigate.NavigateTo("/");
            }
        }
    }
}
