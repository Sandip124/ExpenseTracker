using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ExpenseTracker.Contracts.Dto.Request;
using ExpenseTracker.Contracts.Dto.Response;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Infrastructure.Services
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

        public AuthenticationResponseDto Authenticate(AuthenticationRequestDto model)
        {
            var user = _userRepository.GetQueryable().SingleOrDefault(x => x.Username == model.Username);

            if (user == null) throw new Exception("Username or password does not match.");

            var token = GenerateJwtToken(user);

            return new AuthenticationResponseDto(user.UserId,user.Username, token)
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                RememberMe = model.RememberMe,
                ReturnUrl = model.ReturnUrl
            };
        }
        
        private string GenerateJwtToken(User user)
        {
            var claims = new Claim[] {
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSecret()));
            var signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtHeader = new JwtHeader(signingCredential);
            var jwtPayload = new JwtPayload(claims);
            var token = new JwtSecurityToken(jwtHeader, jwtPayload);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}