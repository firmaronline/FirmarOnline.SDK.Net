using FirmarOnline.Types.Validations;
#if NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace FirmarOnline.Model.Certificates
{
    /// <summary>
    /// Define un certificado.
    /// </summary>
    [JsonConverter(typeof(CertificateJsonConverter))]
    public abstract class Certificate
    {
        /// <summary>
        /// Contraseña en base 64.
        /// </summary>
        [Base64]
        public string Password { get; set; }
    }
}