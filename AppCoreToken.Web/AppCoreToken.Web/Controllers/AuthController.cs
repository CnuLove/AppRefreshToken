using AppToken.Domain.Common;
using AppToken.Infra.Common;
using AppToken.Infra.Interface;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace AppCoreToken.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly ITokenUtils _tokenUtils;
        private readonly string secret = "endrumanbudanseenuendrumanbudanseenu";

        public AuthController(IConfiguration config, IUserService userService,ITokenUtils tokenUtils)
        {
            _config = config;
            _userService = userService;
            _tokenUtils = tokenUtils;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            // Authenticate user
            var user = _userService.Authenticate(loginModel.Username, loginModel.Password);

            if (user == null)
                return Unauthorized();

            // Generate tokens
           // var accessToken = TokenUtils.GenerateAccessToken(user, _config["Jwt:Secret"]);
            var accessToken = _tokenUtils.GenerateAccessToken(user, secret);
            
            var refreshToken = _tokenUtils.GenerateRefreshToken();

            // Save refresh token (for demo purposes, this might be stored securely in a database)
            // _userService.SaveRefreshToken(user.Id, refreshToken);

            var response = new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return Ok(response);
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(TokenResponse tokenResponse)
        {
            // For simplicity, assume the refresh token is valid and stored securely
            // var storedRefreshToken = _userService.GetRefreshToken(userId);

            // Verify refresh token (validate against the stored token)
            // if (storedRefreshToken != tokenResponse.RefreshToken)
            //    return Unauthorized();

            // For demonstration, let's just generate a new access token


            //var newAccessToken = TokenUtils.GenerateAccessTokenFromRefreshToken(tokenResponse.RefreshToken, _config["Jwt:Secret"]);


        

            var newAccessToken = _tokenUtils.GenerateAccessTokenFromRefreshToken(tokenResponse.AccessToken,tokenResponse.RefreshToken, secret);

            

            var response = new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = tokenResponse.RefreshToken // Return the same refresh token
            };

            return Ok(response);
        }
    }
}
