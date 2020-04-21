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
            var persons = Context.Persons
                .Where(o => personIds.Contains(o.ID))
                .Select(o => o.ID)
                .ToList();

            foreach(var id in personIds)
            {
                if (!persons.Contains(id))
                    return false;
            }

            return true;
        }
    }
}
