using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.Data.Repository
{
    public class PersonsRepository : RepositoryBase
    {
        public PersonsRepository(AppDbContext context) : base(context)
        {
        }

        public bool AllPersonsExists(List<long> personIds)
        {
            return Context.Persons.All(o => personIds.Contains(o.ID));
        }
    }
}
