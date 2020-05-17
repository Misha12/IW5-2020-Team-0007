using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.DependencyInjection;
using MovieDatabase.Data.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieDatabase.Data.Validators
{
    public abstract class ExistsValidationBaseAttribute : ValidationAttribute
    {
        public bool AllowNulls { get; set; }
        private Type ItemType { get; }

        protected ExistsValidationBaseAttribute(Type itemType)
        {
            ItemType = itemType;
        }

        public abstract bool RecordsExists(RepositoryBase repository, object value);
        public abstract RepositoryBase GetRepository(ValidationContext context);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null && AllowNulls)
            {
                if (AllowNulls)
                    return ValidationResult.Success;

                return new ValidationResult("NULL data is not allowed.");
            }

            if (!IsValidListType(value))
                return new ValidationResult("Invalid type. Expected list.");

            var repository = GetRepository(validationContext);

            if (!RecordsExists(repository, value))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }

        private bool IsValidListType(object value)
        {
            var type = value.GetType();

            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(List<>))
                return false;

            return type.GenericTypeArguments[0] == ItemType;
        }
    }
}
