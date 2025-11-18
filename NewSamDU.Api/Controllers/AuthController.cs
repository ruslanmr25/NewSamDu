using Microsoft.AspNetCore.Authorization;
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

                var refReshToken = new RefreshToken
                {
                    UserId = user.Id,

                    Token = _tokenService.GenerateRefreshToken(),
                    CreatedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddHours(5),

                    CreatedByIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
                };

                await authRepository.CreateRefreshToken(refReshToken);

                return Ok(
                    new Response()
                    {
                        Data = new
                        {
                            AccessToken = token,
                            RefreshToken = refReshToken.Token,
                            User = new
                            {
                                FullName = user.FullName,
                                Username = user.Username,
                                Role = user.Role.ToString(),
                            },
                        },
                    }
                );
            }

            return Unauthorized(
                new Response() { Success = false, Message = "Login yoki parol xato" }
            );
        }

        [HttpPost("refresh")]
        [Authorize]
        public async Task<IActionResult> Refresh(RefreshTokenDTO dto)
        {
            var user = await authRepository.GetUserByRefreshToken(dto.RefreshToken);

            if (user is null)
            {
                return Unauthorized();
            }

            var oldToken = user.RefreshTokens.Single(x => x.Token == dto.RefreshToken);

            if (oldToken.Expires > DateTime.UtcNow)
            {
                return Unauthorized();
            }

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            RefreshToken token = new RefreshToken
            {
                UserId = user.Id,
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddHours(5),
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "",
            };

            var newAccessToken = _tokenService.GenerateToken(
                user.Id,
                user.FullName,
                user.Role.ToString()
            );

            await authRepository.CreateRefreshToken(token);
            return Ok(
                new Response()
                {
                    Data = new { AccessToken = newAccessToken, RefreshToken = newRefreshToken },
                }
            );
        }
    }
}
