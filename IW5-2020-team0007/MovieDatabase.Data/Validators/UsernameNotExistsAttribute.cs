using Microsoft.Extensions.DependencyInjection;
using MovieDatabase.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Validators
{
    public class UsernameNotExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is string username))
                return new ValidationResult("Neplatný formát uživatelského jména.");

            var repository = validationContext.GetRequiredService<UsersRepository>();

            if (repository.UsernameExists(username))
                return new ValidationResult($"Uživatel {username} již existuje.");

            return ValidationResult.Success;
        }
    }
}
