using ClientSide.Services;
using Microsoft.AspNetCore.Components;

namespace ClientSide.Pages
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService? AuthenticationService { get; set; }
        [Inject]
        public NavigationManager Navigate { get; set; } 

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            Navigate.NavigateTo("/");
        }

    }
}
