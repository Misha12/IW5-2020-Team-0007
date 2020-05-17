using Microsoft.Extensions.DependencyInjection;
using MovieDatabase.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Validators
{
    public class GenreNameNotExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is string name))
                return new ValidationResult("Invalid genre name format.");

            var repository = validationContext.GetService<GenresRepository>();

            if (repository.GenreNameExists(name))
                return new ValidationResult("A genre with this name already exists.");

            return ValidationResult.Success;
        }
    }
}
