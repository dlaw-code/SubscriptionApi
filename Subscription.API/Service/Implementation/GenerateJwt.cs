using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Subscription.API.Service.Interface;
using Subscription.MODEL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Subscription.API.Service.Implementation
{
    public class GenerateJwt : IGenerateJwt
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ServiceUser> _userManager;

        public GenerateJwt(IConfiguration configuration, UserManager<ServiceUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateToken(ServiceUser user)
        {

            var name = user.UserName + " " + user.Email;
            var authClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, user.Id),
        new Claim(ClaimTypes.Name, name),
        new Claim(ClaimTypes.UserData, user.Id)
    };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddYears(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha384Signature));
            var Jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
            return Jwttoken;
        }
    }
}
