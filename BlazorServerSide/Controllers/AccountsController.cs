using BlazorServerSide.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerSide.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserForRegistrationDto userForRegistiration)
        {
            try
            {
                var user = new IdentityUser { UserName = userForRegistiration.Email , Email = userForRegistiration.Email };
                var result = await _userManager.CreateAsync(user,userForRegistiration.Password);

                
                if(!result.Succeeded) 
                {
                    var erros = result.Errors.Select(e => e.Description);
                    return BadRequest(new RegistrationResponseDto { Errors= erros });   
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new ServiceResponse();
                response.Success = false;
                response.Message= ex.Message;   
                return BadRequest(response);    
            }
        }
    }
}
