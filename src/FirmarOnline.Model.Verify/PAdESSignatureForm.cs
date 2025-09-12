namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Define las formas técnicas de una firma PAdES según el estándar ETSI EN 319 142-1.
    /// Cada forma representa un nivel de mejora sobre la firma básica, con elementos como políticas, sellado de tiempo,
    /// información de validación (OCSP/CRL), y mecanismos de archivo a largo plazo.
    /// </summary>
    public enum PadesSignatureForm
    {
        /// <summary>
        /// Firma no PAdES o forma no reconocida/soportada.
        /// </summary>
        Unknown,

        /// <summary>
        /// PAdES-BES (Basic Electronic Signature): firma electrónica avanzada con el certificado del firmante.
        /// Es la forma básica requerida para una firma legalmente válida.
        /// </summary>
        BES,

        /// <summary>
        /// PAdES-EPES (Explicit Policy-based Electronic Signature): incluye políticas de firma explícitas,
        /// como identificadores de política o requisitos específicos del firmante.
        /// </summary>
        EPES,

        /// <summary>
        /// PAdES-T (Timestamp): añade un sello de tiempo confiable a la firma.
        /// Garantiza la existencia del documento en un momento determinado.
        /// </summary>
        T,

        /// <summary>
        /// PAdES-C: incorpora referencias a la cadena de certificados del firmante.
        /// Mejora la verificabilidad de la firma.
        /// </summary>
        C,

        /// <summary>
        /// PAdES-X: incluye información de validación (OCSP o CRL) sobre los certificados usados.
        /// Permite comprobar la validez en el momento de la firma.
        /// </summary>
        X,

        /// <summary>
        /// PAdES-XL: añade toda la información de validación (certificados y OCSP/CRL),
        /// permitiendo validación sin conexión a fuentes externas.
        /// </summary>
        XL,

        /// <summary>
        /// PAdES-A (Archival): incorpora sellos de tiempo sucesivos para mantener
        /// la validez a largo plazo. Diseñada para archivado legal.
        /// </summary>
        A
    }
}