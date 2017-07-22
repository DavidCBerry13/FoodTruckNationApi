using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework
{
    public class StringCollectionValidationAttribute : ValidationAttribute
    {


        private readonly String regex;

        public StringCollectionValidationAttribute(String regex)
        {
            this.regex = regex;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            
            var items = value as IEnumerable<String>;
            if (items == null)
                throw new ValidationException("This attribute can only be applied to collections of strings");
           
            var invalidItems = items.Where(s => !Regex.IsMatch(s, regex) ).ToArray();
            if (invalidItems.Length > 0)
                return new ValidationResult("The following items do not match the regex: " + string.Join(", ", invalidItems));

            return ValidationResult.Success;
        }

    }
}
