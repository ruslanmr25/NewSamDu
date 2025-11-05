using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewSamDU.Application.Abstractions.IServices;
using NewSamDU.Application.DTOs.AuthDTOs;
using NewSamDU.Application.Responses;
using NewSamDU.Application.Services;
using NewSamDU.Domain.Entities;
using NewSamDU.Infrastructure.Repositories;

namespace NewSamDU.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected readonly AuthRepository authRepository;

        protected readonly TokenService _tokenService;

        protected readonly IPasswordHasher passwordHasher;

        public AuthController(
            IPasswordHasher passwordHasher,
            AuthRepository authRepository,
            TokenService tokenService
        )
        {
            this.passwordHasher = passwordHasher;
            this.authRepository = authRepository;
            this._tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            User? user = await authRepository.GetUser(request.Username);

            if (user is not null && passwordHasher.Verify(request.Password, user.Password))
            {
                var token = _tokenService.GenerateToken(
                    user.Id,
                    user.FullName,
                    user.Role.ToString()
                );

                return Ok(new Response() { Data = new { Token = token } });
            }

            return Unauthorized(
                new Response() { Success = false, Message = "Login yoki parol xato" }
            );
        }
    }
}
