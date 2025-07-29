using System;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.Validations
{
    /// <summary>
    /// Validation attribute for checking if base64 content matches with a jpg image
    /// </summary>
    public class Base64JPGAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Base64JPGAttribute"/>
        /// </summary>
        public Base64JPGAttribute()
        {
            ErrorMessage = "The content is not a valid JPG Image";
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Base64JPGAttribute"/>
        /// class by using the function that enables access to validation resources
        /// </summary>
        /// <param name="errorMessageAccessor">The funciton that enables access to validation resources</param>
        public Base64JPGAttribute(Func<string> errorMessageAccessor)
            : base(errorMessageAccessor) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Base64JPGAttribute"/>
        /// class by using the error message to associate with a validation control
        /// </summary>
        /// <param name="errorMessage">The error message to associate with a validation control</param>
        public Base64JPGAttribute(string errorMessage)
            : base(errorMessage) { }

        /// <summary>
        /// Checks if the content begins with the JPG string 
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
            return StringValidator<Base64JPGValidationType>.IsValid(b64Content);
        }
    }
}
