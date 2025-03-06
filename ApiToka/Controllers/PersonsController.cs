using ApiToka.Models;
using AutoMapper;
using Core.DTOs;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiToka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {

        private readonly IPersonsService personsService;
        private readonly IMapper mapper;

        public PersonsController(IPersonsService personsService, IMapper mapper, IConfiguration config)
        {
            this.personsService = personsService;
            this.mapper = mapper;
        }

        // GET: api/<PersonsController>
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<PersonDTO>> Get()
        {
            try
            {
                var persons = personsService.GetAllPersons();
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error interno", detalle = ex.Message });
            }
        }

        // GET api/<PersonsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var persons = personsService.GetPerson(id);
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error interno", detalle = ex.Message });
            }
        }

        // POST api/<PersonsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] PersonRequest personRequest)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var person = new PersonDTO
                    {
                        Nombre = personRequest.Nombre,
                        ApellidoPaterno = personRequest.ApellidoPaterno,
                        ApellidoMaterno = personRequest.ApellidoMaterno,
                        RFC = personRequest.RFC,
                        FechaNacimiento = personRequest.FechaNacimiento,
                        Activo = Convert.ToBoolean(personRequest.Activo)
                    };
                    personsService.AddPerson(person);

                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error interno", detalle = ex.Message });
            }

        }

        // PUT api/<PersonsController>
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] PersonRequest personRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (!personRequest.Id.HasValue)
                    {
                        return BadRequest("El Id es obligatorio para la actualización");
                    }

                    var person = new PersonDTO
                    {
                        IdPersonaFisica = (int)personRequest.Id,
                        Nombre = personRequest.Nombre,
                        ApellidoPaterno = personRequest.ApellidoPaterno,
                        ApellidoMaterno = personRequest.ApellidoMaterno,
                        RFC = personRequest.RFC,
                        FechaNacimiento = personRequest.FechaNacimiento,
                        Activo = Convert.ToBoolean(personRequest.Activo)
                    };
                    personsService.UpdatePerson(person);

                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error interno", detalle = ex.Message });
            }
        }

        // DELETE api/<PersonsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                personsService.ChangeStatusPerson(id, 0);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error interno", detalle = ex.Message });
            }
        }
    }
}
