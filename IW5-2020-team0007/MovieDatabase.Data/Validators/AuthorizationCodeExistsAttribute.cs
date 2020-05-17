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
                return new ValidationResult("Invalid authorization code format.");

            var repository = validationContext.GetRequiredService<UsersRepository>();

            if (!repository.AuthCodeExists(code))
                return new ValidationResult("An invalid authorization code was entered. The code either doesn't exist or has already been used.");

            return ValidationResult.Success;
        }
    }
}
