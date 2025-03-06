using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class PersonsRepository : GenericRepository<Person>, IPersonsRepository
    {
        public PersonsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
