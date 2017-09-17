using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework
{
    /// <summary>
    /// Validates all strings in a collection match the provided regex
    /// </summary>
    /// <remarks>
    /// This attribute does not validate that there are any elements in the collection, only
    /// that any elements contained in the collection match the regex.  
    /// </remarks>
    public class StringCollectionValidationAttribute : ValidationAttribute
    {

        public StringCollectionValidationAttribute(String pattern)
        {
            this.Pattern = pattern;
            this.MatchTimeoutInMilliseconds = 2000;
        }

        /// <summary>
        ///     Gets or sets the timeout to use when matching the regular expression pattern (in milliseconds)
        ///     (-1 means never timeout).
        /// </summary>
        public int MatchTimeoutInMilliseconds { get; set; }

        /// <summary>
        ///     Gets the regular expression pattern to use
        /// </summary>
        public string Pattern { get; private set; }

        private Regex Regex { get; set; }




        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            
            if (value == null)
                throw new InvalidOperationException("Cannot validate null object.  Did you forget to instantiate the collection?");

            if (value is IEnumerable<String> == false)
                throw new InvalidOperationException("This attribute can only be applied to collections of strings");

            this.SetupRegex();

            var items = value as IEnumerable<String>;           
            var invalidItems = items.Where(s => !this.Regex.IsMatch(s) ).ToArray();
            if (invalidItems.Length > 0)
                //return new ValidationResult("The following items do not match the regex: " + string.Join(", ", invalidItems));
                return new ValidationResult(FormatErrorMessage(validationContext?.DisplayName));

            return ValidationResult.Success;
        }
       


        /// <summary>
        ///     Sets up the <see cref="Regex" /> property from the <see cref="Pattern" /> property.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown if the current <see cref="Pattern" /> cannot be parsed</exception>
        /// <exception cref="InvalidOperationException"> is thrown if the current attribute is ill-formed.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> thrown if <see cref="MatchTimeoutInMilliseconds" /> is negative (except -1),
        /// zero or greater than approximately 24 days </exception>
        private void SetupRegex()
        {
            if (Regex == null)
            {
                if (string.IsNullOrEmpty(Pattern))
                {
                    throw new InvalidOperationException("An empty/null regex pattern was passed to StringCollectionValidationAttribute");
                }

                Regex = MatchTimeoutInMilliseconds == -1
                    ? new Regex(Pattern)
                    : new Regex(Pattern, default(RegexOptions), TimeSpan.FromMilliseconds((double)MatchTimeoutInMilliseconds));
            }
        }


    }
}
