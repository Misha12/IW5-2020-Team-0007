using Microsoft.Extensions.DependencyInjection;
using MovieDatabase.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Validators
{
    public class AuthorizationCodeExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is string code))
                return new ValidationResult("Neplatný formát autorizačního kódu.");

            var repository = validationContext.GetRequiredService<UsersRepository>();

            if (!repository.AuthCodeExists(code))
                return new ValidationResult("Byl zadán neplatný autorizační kód. Kód buď neexistuje, nebo již byl použit.");

            return ValidationResult.Success;
        }
    }
}
