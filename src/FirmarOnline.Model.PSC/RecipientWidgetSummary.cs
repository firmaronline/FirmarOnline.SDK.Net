using FirmarOnline.Types.Widgets;

namespace FirmarOnline.Model.PSC /*OJO*/ // Debería ser privado?
{
    /// <summary>
    /// Resumen de información del Widget.
    /// </summary>
    public class RecipientWidgetSummary
    {
        /// <summary>
        /// Altura de la caja
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Ancho de la caja
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Texto a localizar en el documento para posicionar la caja de firma
        /// </summary>
        public string TextToSearch { get; set; }

        /// <summary>
        /// Número de página
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Desplazamiento horizontal
        /// </summary>
        public int? PosX { get; set; }

        /// <summary>
        /// Desplazamiento vertical
        /// </summary>
        public int? PosY { get; set; }

        /// <summary>
        /// Nombre del campo del documento que define
        /// la ubicación de la caja de firma
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Rotación de la caja
        /// </summary>
        public RotationType? Rotation { get; set; }
    }
}