using Blazored.LocalStorage;
using ClientSide.AuthProviders;
using Entities.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ClientSide.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly JsonSerializerOptions _options;

        public AuthenticationService(HttpClient client,AuthenticationStateProvider authStateProvider,ILocalStorageService localStorage)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _options =new JsonSerializerOptions { PropertyNameCaseInsensitive=true };
        }

        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            var content=JsonSerializer.Serialize(userForAuthentication);
            var bodyContent= new StringContent(content,Encoding.UTF8,"application/json");

            var authResult = await _client.PostAsync("Login", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result= JsonSerializer.Deserialize<AuthResponseDto>(authContent,_options);

            if (!authResult.IsSuccessStatusCode)
                return result;
            await _localStorage.SetItemAsync("authToken", result.Token);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuth(userForAuthentication.Email);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return new AuthResponseDto { IsAuthSuccessful = true };

        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null; 
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistiration)
        {
            var content = JsonSerializer.Serialize(userForRegistiration);
            var bodyContent=new StringContent(content,Encoding.UTF8,"application/json");

            var registrationResult = await _client.PostAsync("Register", bodyContent);
            var registrationContent= await registrationResult.Content.ReadAsStringAsync();

            if(!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, _options);
                return result;
            }
            return new RegistrationResponseDto { IsSuccessfulRegistiration= true };    
        }
    }
}
