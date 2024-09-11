using AppToken.Domain.Common;
using AppToken.Infra.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppToken.Infra.Common
{
    public class TokenUtils : ITokenUtils
    {
        private readonly IConfiguration _configuration;
        public TokenUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken(User user, string secret)
        {

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSetting:Key"]));

            var signcredentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Roles)
            }.ToList();

            var Token = new JwtSecurityToken(
                issuer: _configuration["JwtSetting:Issuer"],
                audience: _configuration["JwtSetting:Audience"],
                claims: claims,
                signingCredentials: signcredentials,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JwtSetting:DurationInMinutes"]))
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string GenerateAccessTokenFromRefreshToken(string Token, string refreshToken, string secret)
        {
            // Implement logic to generate a new access token from the refresh token
            // Verify the refresh token and extract necessary information (e.g., user ID)
            // Then generate a new access token

            // For demonstration purposes, return a new token with an extended expiry


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var authtoken = new JwtSecurityTokenHandler().ReadJwtToken(Token);

            string _issuer = authtoken.Claims.FirstOrDefault(c => c.Type == "iss").Value.ToString();
            string _name = authtoken.Claims.FirstOrDefault(c => c.Type.Contains("name") == true).Value.ToString();
            string _audience = authtoken.Claims.FirstOrDefault(c => c.Type == "aud").Value.ToString();
            string _role = authtoken.Claims.FirstOrDefault(c => c.Type.Contains("role") == true).Value.ToString();

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSetting:Key"]));

            var signcredentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>() {
            new Claim(ClaimTypes.Name, _name),
            new Claim(ClaimTypes.Role,_role)
            }.ToList();

            var Tokens = new JwtSecurityToken(
                issuer: _configuration["JwtSetting:Issuer"],
                audience: _configuration["JwtSetting:Audience"],
                claims: claims,
                signingCredentials: signcredentials,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JwtSetting:DurationInMinutes"]))
                );

            return new JwtSecurityTokenHandler().WriteToken(Tokens);



        }
    }
}
