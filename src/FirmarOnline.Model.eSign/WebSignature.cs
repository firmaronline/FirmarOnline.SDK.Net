using FirmarOnline.Types.Validations;

namespace FirmarOnline.Model.eSign
{
    /// <summary>
    /// Definición de datos para la firma de un documento con datos biométricos
    /// </summary>
    public class WebSignature : Signature
    {
        /// <summary>
        /// Datos públicos de la firma biométrica
        /// </summary>
        [Base64]
        public string B64PublicBioData { get; set; }

        /// <summary>
        /// Datos privados de la firma biométrica
        /// </summary>
        [Base64]
        public string B64PrivateBioData { get; set; }

        /// <summary>
        /// Identificador del dispositivo.
        /// </summary>
        public string DeviceId { get; set; }
    }
}