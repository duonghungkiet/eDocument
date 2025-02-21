using eDocument.Domain.Entities;
using eDocument.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace eDocument.Application.Features.Auth.Commands
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthenticationResponse>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public LoginHandler(
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var isValidPassword = user.VerifyPassword(request.Password, _passwordHasher);
            if (!isValidPassword)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = RefreshTokenGenerator.GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            user.SetRefreshToken(refreshToken, refreshTokenExpiry);
            await _userRepository.UpdateAsync(user);

            return new AuthenticationResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiry = refreshTokenExpiry
            };
        }
    }

    public static class RefreshTokenGenerator
    {
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public class AuthenticationResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
