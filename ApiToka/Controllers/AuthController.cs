using ApiToka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiToka.Controllers
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("GetToken")]
        public IActionResult GetToken(AuthRequest authRequest)
        {

            if (authRequest.Username == null || authRequest.Password == null)
            {
                return BadRequest();
            }

            if (authRequest.Username == configuration.GetValue<string>("Auth:Username")
                && authRequest.Password == configuration.GetValue<string>("Auth:Password"))
            {
                var issuer = configuration.GetValue<string>("Jwt:Issuer");
                var audience = configuration.GetValue<string>("Jwt:Audience");
                var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Jwt:Token"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([
                        new Claim(JwtRegisteredClaimNames.Sub, authRequest.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    ]),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);

                return Ok(stringToken);

            }
            else
            {
                return Unauthorized();
            }

        }
    }
}
