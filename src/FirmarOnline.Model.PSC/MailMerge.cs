using FirmarOnline.Model.Validations;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Definición de la combinación de correspondencia
    /// </summary>
    public class MailMerge
    {
        /// <summary>
        /// Nombre de la combinación de correspondencia
        /// </summary>
        [Required]
        [StringLength(255)]
        public string MailMergeName { get; set; }

        /// <summary>
        /// Identificador de flujo
        /// </summary>
        [Required]
        [StringLength(80)]
        public string FlowTokenId { get; set; }

        /// <summary>
        /// Identificador de equipo
        /// </summary>
        [StringLength(80)]
        public string TeamId { get; set; }

        /// <summary>
        /// Nombre del sobre
        /// </summary>
        [Required]
        [StringLength(255)]
        public string DocumentSetName { get; set; }

        /// <summary>
        /// Referencia externa de sobre para cliente
        /// </summary>
        [StringLength(64)]
        public string Reference { get; set; }

        /// <summary>
        /// Nombre del documento
        /// </summary>
        [Required]
        [StringLength(255)]
        public string DocumentName { get; set; }

        /// <summary>
        /// Tipo de documento de datos de recipients.
        /// Si no se informa se asumirá que es un fichero de tipo Excel.
        /// </summary>
        public MailMergeDataFileType DocumentType { get; set; }

        /// <summary>
        /// Contenido del archivo de datos (Excel o CSV) en base 64 con la información de los destinatarios.
        /// </summary>
        [Required]
        [Base64]
        public string B64RecipientData { get; set; }
    }
}