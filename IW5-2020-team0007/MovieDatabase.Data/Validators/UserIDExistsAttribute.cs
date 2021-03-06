﻿using Microsoft.Extensions.DependencyInjection;
using MovieDatabase.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Validators
{
    public class UserIDExistsAttribute : ValidationAttribute
    {
        public bool AllowNull { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                if (AllowNull)
                    return ValidationResult.Success;

                return new ValidationResult("Invalid input format.");
            }

            if (!(value is long id))
                return new ValidationResult("Invalid input format. Expected number.");

            using var repository = validationContext.GetRequiredService<UsersRepository>();

            if (!repository.UserIDExists(id))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
