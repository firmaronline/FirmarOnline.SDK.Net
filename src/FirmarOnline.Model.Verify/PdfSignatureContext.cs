namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Define el tipo estructural de firma electrónica contenida en un documento PDF.
    /// Cada valor representa un tipo de firma diferente según su propósito técnico:
    /// desde firmas documentales estándar hasta firmas certificadas (MDP),
    /// control de derechos de uso o firmas incrustadas en objetos.
    /// </summary>
    public enum PdfSignatureContext
    {
        /// <summary>
        /// Firma electrónica de un documento PDF normal.
        /// Es la forma más común (firma visible o no) que garantiza integridad
        /// sin impedir modificaciones posteriores.
        /// </summary>
        Document,

        /// <summary>
        /// Firma certificada (MDP - Modification Detection and Prevention).
        /// Utilizada para proteger el documento completo contra cambios,
        /// define el nivel de permisos permitidos tras la firma.
        /// </summary>
        MDP,

        /// <summary>
        /// Firma que otorga o restringe derechos de uso sobre el documento
        /// (por ejemplo, rellenar formularios, guardar, imprimir).
        /// Típicamente usada en combinación con control DRM o LiveCycle.
        /// </summary>
        UsageRights,

        /// <summary>
        /// Firma aplicada a un objeto PDF específico (como una firma embebida en un formulario o anotación).
        /// Menos común y de uso especializado.
        /// </summary>
        Object,

        /// <summary>
        /// Tipo de firma no reconocido o no compatible con el sistema de validación.
        /// </summary>
        Unknown
    }
}