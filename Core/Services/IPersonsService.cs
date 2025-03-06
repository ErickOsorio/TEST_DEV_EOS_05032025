using Core.DTOs;

namespace Core.Services
{
    public interface IPersonsService
    {
        public IEnumerable<PersonDTO> GetPersons();

        void AddPerson(PersonDTO personDTO);

        void UpdatePerson(PersonDTO personDTO);

        void ChangeStatusPerson(int personId, int status);
    }
}
