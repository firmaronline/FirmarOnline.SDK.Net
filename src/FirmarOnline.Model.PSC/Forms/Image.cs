#if NET6_0_OR_GREATER
namespace FirmarOnline.Model.PSC.Forms
{
    /// <summary>
    /// Imagen.
    /// </summary>
    public class Image : ItemBase
    {
        /// <summary>
        /// Fuente (para imágenes).
        /// </summary>
        public string Src { get; set; }
    }
}
#endif