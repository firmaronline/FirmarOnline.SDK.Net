#if NET6_0_OR_GREATER

namespace FirmarOnline.Model.Forms
{
    /// <summary>
    /// Campo de tipo texto.
    /// </summary>
    public class TextField : StringField
    {
        ///// <summary>
        ///// Valor.
        ///// </summary>
        //[MaxLength(255)]
        //public string Value { get; set; }

        /// <summary>
        /// Reglas a cumplir en el campo
        /// </summary>
        public string Rules { get; set; }
    }
}

#endif