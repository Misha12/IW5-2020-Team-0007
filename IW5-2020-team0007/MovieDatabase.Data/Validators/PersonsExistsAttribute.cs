using Microsoft.Extensions.DependencyInjection;
using MovieDatabase.Data.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Validators
{
    public class PersonsExistsAttribute : ExistsValidationBaseAttribute
    {
        public PersonsExistsAttribute() : base(typeof(long))
        {
        }

        public override RepositoryBase GetRepository(ValidationContext context)
        {
            return context.GetRequiredService<PersonsRepository>();
        }

        public override bool RecordsExists(RepositoryBase repository, object value)
        {
            var list = (List<long>)value;
            return ((PersonsRepository)repository).AllPersonsExists(list);
        }
    }
}
