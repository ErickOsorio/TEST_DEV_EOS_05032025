using ApiToka.Models;
using AutoMapper;
using Core.DTOs;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiToka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {

        private readonly IPersonsService personsService;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public PersonsController(IPersonsService personsService, IMapper mapper, IConfiguration config)
        {
            this.personsService = personsService;
            this.mapper = mapper;
            this.config = config;
        }

        // GET: api/<PersonsController>
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<PersonDTO>> Get()
        {
            try
            {
                var persons = personsService.GetPersons();
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PersonsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PersonsController>
        [Authorize]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PersonsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("Auth/GetToken")]
        [HttpPost]
        public IActionResult GetToken(AuthRequest authRequest)
        {

            if (authRequest.Username == null || authRequest.Password == null)
            {
                return Unauthorized();
            }

            if (authRequest.Username == config.GetValue<string>("Auth:Username")
                && authRequest.Password == config.GetValue<string>("Auth:Password"))
            {
                var issuer = config.GetValue<string>("Jwt:Issuer");
                var audience = config.GetValue<string>("Jwt:Audience");
                var key = Encoding.ASCII.GetBytes(config.GetValue<string>("Jwt:Key"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, authRequest.Username),
                        new Claim(JwtRegisteredClaimNames.Email, authRequest.Username),
                        new Claim(JwtRegisteredClaimNames.Jti,                                                                                                                                                                                                                                                                                              Guid.NewGuid().ToString())
                    ]),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);

                return Ok(stringToken);

            }
            else
            {
                return BadRequest();
            }

        }
    }
}
