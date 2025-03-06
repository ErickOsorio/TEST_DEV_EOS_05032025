using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Services;

namespace Infrastructure.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly IMapper mapper;
        private IPersonsRepository personsRepository;

        public PersonsService(IMapper mapper, IPersonsRepository personsRepository)
        {
            this.mapper = mapper;
            this.personsRepository = personsRepository;
        }

        public void AddPerson(PersonDTO personDTO)
        {
            personDTO.FechaRegistro = DateTime.Now;
            personDTO.FechaActualizacion = DateTime.Now;

            personsRepository.Add(mapper.Map<Person>(personDTO));
            personsRepository.Save();
        }

        public void ChangeStatusPerson(int personId, int status)
        {
            PersonDTO person = new()
            {
                IdPersonaFisica = personId,
                FechaActualizacion = DateTime.Now,
                Activo = Convert.ToBoolean(status),
            };
            personsRepository.Update(mapper.Map<Person>(person), p => p.FechaActualizacion, p => p.Activo);
            personsRepository.Save();

        }

        public IEnumerable<PersonDTO> GetAllPersons()
        {
            var persons = personsRepository.GetAll();
            return mapper.Map<IEnumerable<PersonDTO>>(persons);
        }

        public PersonDTO GetPerson(int Id)
        {
            var persons = personsRepository.GetById(Id);
            return mapper.Map<PersonDTO>(persons);
        }

        public void UpdatePerson(PersonDTO personDTO)
        {
            personDTO.FechaActualizacion = DateTime.Now;

            personsRepository.Update(mapper.Map<Person>(personDTO), p => p.Nombre, p => p.ApellidoPaterno, p => p.ApellidoMaterno,
                p => p.RFC, p => p.FechaNacimiento, p => p.Activo, p => p.FechaActualizacion);
            personsRepository.Save();
        }
    }
}
