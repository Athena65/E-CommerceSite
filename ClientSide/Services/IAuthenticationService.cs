﻿using Entities.DTO;

namespace ClientSide.Services
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistiration);

    }
}