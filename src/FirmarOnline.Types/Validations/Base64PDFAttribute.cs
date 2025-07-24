using System;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Types.Validations
{
    /// <summary>
    /// Validation attribute for checking if base64 content matches with a pdf file
    /// </summary>
    public class Base64PDFAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Base64PDFAttribute"/>
        /// </summary>
        public Base64PDFAttribute()
        {
            ErrorMessage = "The content is not a valid PDF file";
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Base64PDFAttribute"/>
        /// class by using the function that enables access to validation resources
        /// </summary>
        /// <param name="errorMessageAccessor">The funciton that enables access to validation resources</param>
        public Base64PDFAttribute(Func<string> errorMessageAccessor)
            : base(errorMessageAccessor) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Base64PDFAttribute"/>
        /// class by using the error message to associate with a validation control
        /// </summary>
        /// <param name="errorMessage">The error message to associate with a validation control</param>
        public Base64PDFAttribute(string errorMessage)
            : base(errorMessage) { }

        /// <summary>
        /// Checks if the content begins with the PDF header string %PDF-
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

            return StringValidator<Base64PDFValidationType>.IsValid(b64Content);
        }

    }
}
