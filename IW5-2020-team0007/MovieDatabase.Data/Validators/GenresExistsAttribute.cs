using Microsoft.Extensions.DependencyInjection;
using MovieDatabase.Data.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Validators
{
    public class GenresExistsAttribute : ExistsValidationBaseAttribute
    {
        public GenresExistsAttribute() : base(typeof(int))
        {
        }

        public override RepositoryBase GetRepository(ValidationContext context)
        {
            return context.GetRequiredService<GenresRepository>();
        }

        public override bool RecordsExists(RepositoryBase repository, object value)
        {
            var list = (List<int>)value;
            return ((GenresRepository)repository).AllGenresExists(list);
        }
    }
}
