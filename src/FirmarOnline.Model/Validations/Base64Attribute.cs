using System;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.Validations
{
    /// <summary>
    /// Validation attribute for checking if base64 content is valid
    /// </summary>
    public class Base64Attribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Base64Attribute"/>
        /// </summary>
        public Base64Attribute()
        {
            ErrorMessage = "The content is not a valid base64 string";
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Base64Attribute"/>
        /// class by using the function that enables access to validation resources
        /// </summary>
        /// <param name="errorMessageAccessor">The funciton that enables access to validation resources</param>
        public Base64Attribute(Func<string> errorMessageAccessor)
            : base(errorMessageAccessor) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Base64Attribute"/>
        /// class by using the error message to associate with a validation control
        /// </summary>
        /// <param name="errorMessage">The error message to associate with a validation control</param>
        public Base64Attribute(string errorMessage)
            : base(errorMessage) { }

        /// <summary>
        /// Checks if the content is base64 valid
        /// </summary>
        /// <param name="value">The property value to check</param>
        /// <returns>True if the header matches, otherwise false</returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if (!(value is string b64Content))
            {
                return false;
            }

            return StringValidator<Base64ValidationType>.IsValid(b64Content);
        }

    }
}
