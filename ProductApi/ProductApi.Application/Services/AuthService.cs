// src/ProductApi.Application/Services/AuthService.cs
using Microsoft.IdentityModel.Tokens;
using ProductApi.Core.Entities;
using ProductApi.Core.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ProductApi.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || user.Password != password)  // In a real app, use a password hash comparison
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task Register(string username, string password)
        {
            _logger.LogInformation("Fetching products from API..." + username + "-" + password);
            var user = new User { Username = username, Password = password };  // In a real app, hash the password
            await _userRepository.AddAsync(user);
        }
    }
}
