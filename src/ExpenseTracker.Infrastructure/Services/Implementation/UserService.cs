using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ExpenseTracker.Core.Dto;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Infrastructure.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration,IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public AuthenticateResponseDto Authenticate(AuthenticateRequestDto model)
        {
            var user = _userRepository.GetQueryable().SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null) throw new Exception("Username or password does not match.");

            var token = GenerateJwtToken(user);

            return new AuthenticateResponseDto(user, token)
            {
                RememberMe = model.RememberMe,
                ReturnUrl = model.ReturnUrl
            };
        }
        
        private string GenerateJwtToken(User user)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSecret()));
            SigningCredentials signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            JwtHeader jwtHeader = new JwtHeader(signingCredential);
            JwtPayload jwtPayload = new JwtPayload(claims);
            JwtSecurityToken token = new JwtSecurityToken(jwtHeader, jwtPayload);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}