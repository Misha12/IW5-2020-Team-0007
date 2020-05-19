using Microsoft.Extensions.DependencyInjection;
using MovieDatabase.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Validators
{
    public class MovieIDExistsAttribute : ValidationAttribute
    {
        public bool AllowNull { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                if (AllowNull)
                    return ValidationResult.Success;

                return new ValidationResult("Invalid input format.");
            }

            if (!(value is long id))
                return new ValidationResult("Invalid input format. Expected number.");

            var repository = validationContext.GetRequiredService<MoviesRepository>();

            if (!repository.ExistsMovie(id))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
