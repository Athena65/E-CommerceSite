using Entities.DTO;
using System.Text;
using System.Text.Json;

namespace ClientSide.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public AuthenticationService(HttpClient client)
        {
            _client = client;
            _options=new JsonSerializerOptions { PropertyNameCaseInsensitive=true };
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
