using ClientSide.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Components;

namespace ClientSide.Pages
{
    public partial class Registration
    {
        private UserForRegistrationDto _userForRegistration=new UserForRegistrationDto();
        [Inject]
        public IAuthenticationService? AuthService { get; set; }
        [Inject]
        public NavigationManager? Navigate { get; set; }
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public async Task Register()
        {
            ShowRegistrationErrors= false;  
            var result=await AuthService.RegisterUser(_userForRegistration);
            if(!result.IsSuccessfulRegistiration)
            {
                Errors = result.Errors;
                ShowRegistrationErrors= true;
            }
            else
            {
                Navigate.NavigateTo("/");
            }
        }
    }
}
