namespace FootyLeague.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.Data.Models;
    using FootyLeague.Services.Data.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._configuration = configuration;
        }

        public JwtSecurityToken GenerateJwtToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JwtSettings:SecretKey"]));

            return new JwtSecurityToken(
                issuer: this._configuration["JwtSettings:Issuer"],
                audience: this._configuration["JwtSettings:Audience"],
                expires: DateTime.Now.AddMinutes(15),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }

        public async Task<string> CreateRefreshToken(ApplicationUser user)
        {
            var refreshToken = this.GenerateRefreshToken(user);
            await this._userManager.SetAuthenticationTokenAsync(user, "JWT", "RefreshToken", refreshToken);
            return refreshToken;
        }

        public async Task<string> GetStoredRefreshToken(ApplicationUser user)
        {
            return await this._userManager.GetAuthenticationTokenAsync(user, "JWT", "RefreshToken");
        }

        public async Task RemoveRefreshToken(ApplicationUser user)
        {
            await this._userManager.RemoveAuthenticationTokenAsync(user, "JWT", "RefreshToken");

        }

        private string GenerateRefreshToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(12);

            var token = new JwtSecurityToken(
                this._configuration["JwtSettings:Issuer"],
                this._configuration["JwtSettings:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
