using FirmarOnline.Model.Certificates;
using FirmarOnline.Model.Validations;
using FirmarOnline.Model.Widgets;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.eSign
{
    /// <summary>
    /// Definición de datos para la firma de un documento mediante certificado electrónico
    /// </summary>
    [CustomValidation(typeof(Signature), nameof(ValidateSignature),
        ErrorMessage = "The signature definition is not valid.")]
    public class Signature
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="Signature"/>
        /// </summary>
        public Signature()
        {
            Options = new SignatureOptions();
        }

        /// <summary>
        /// Documento
        /// </summary>
        [Required]
        [Base64PDF]
        public string B64PDFContent { get; set; }

        /// <summary>
        /// Definición de la caja de firma
        /// </summary>
        [Required]
        public Widget Widget { get; set; }

        /// <summary>
        /// Certificado de firma
        /// </summary>
        public Certificate Certificate { get; set; }

        /// <summary>
        /// Opciones de firma
        /// </summary>
        public SignatureOptions Options { get; set; }

        /// <summary>
        /// Propiedades de la firma
        /// </summary>
        public SignatureProperties Properties { get; set; }

        /// <summary>
        /// Permite indicar un proveedor externo para el sellado de tiempo
        /// </summary>
        public ExternalProvider TimestampProvider { get; set; }

        /// <summary>
        /// Permite indicar un proveedor OCSP externo para la validación
        /// del certificado
        /// </summary>
        public ExternalProvider OCSPProvider { get; set; }

        /// <summary>
        /// Validación de datos para la firma de un documento mediante certificado electrónico
        /// </summary>
        /// <param name="signature">Datos para la firma</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateSignature(Signature signature)
        {
            // Tsp
            if (signature.Options.IncludeTimestamp && signature.TimestampProvider != null && string.IsNullOrEmpty(signature.TimestampProvider.Url))
            {
                return new ValidationResult("The parameter url must be informed.",
                        [nameof(TimestampProvider)]);
            }

            // Ocsp
            if (signature.Options.IncludeOCSP && signature.OCSPProvider != null && string.IsNullOrEmpty(signature.OCSPProvider.Url))
            {
                return new ValidationResult("The parameter url must be informed.",
                        [nameof(OCSPProvider)]);
            }

            // Propiedades
            if (signature.Properties != null)
            {
                if (string.IsNullOrEmpty(signature.Properties.Author))
                {
                    return new ValidationResult("The parameter author must be informed.",
                        [nameof(Properties)]);
                }
                if (string.IsNullOrEmpty(signature.Properties.Reason))
                {
                    return new ValidationResult("The parameter reason must be informed.",
                        [nameof(Properties)]);
                }
                if (string.IsNullOrEmpty(signature.Properties.Contact))
                {
                    return new ValidationResult("The parameter contact must be informed.",
                        [nameof(Properties)]);
                }
                if (string.IsNullOrEmpty(signature.Properties.Location))
                {
                    return new ValidationResult("The parameter location must be informed.",
                        [nameof(Properties)]);
                }
            }

            // Certificado
            if (signature.Certificate != null)
            {
                if (signature.Certificate is PKCS8Certificate pkcs8Certificate)
                {
                    if (string.IsNullOrEmpty(pkcs8Certificate.P8PublicCert))
                    {
                        return new ValidationResult("The parameter p8PublicCert must be informed.",
                        [nameof(Certificate)]);
                    }
                    else if (string.IsNullOrEmpty(pkcs8Certificate.P8PrivateKey))
                    {
                        return new ValidationResult("The parameter must p8PrivateKey be informed.",
                        [nameof(Certificate)]);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
