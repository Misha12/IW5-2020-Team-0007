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
                return new ValidationResult("Neplatný formát názvu žánru.");

            var repository = validationContext.GetService<GenresRepository>();

            if (repository.GenreNameExists(name))
                return new ValidationResult("Žánr s tímto jménem již existuje.");

            return ValidationResult.Success;
        }
    }
}
