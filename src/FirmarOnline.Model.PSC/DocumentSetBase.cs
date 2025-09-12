using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Clase base para la definición de sobres de documentos
    /// para el módulo de firma remota.
    /// </summary>
    public abstract class DocumentSetBase
    {
        /// <summary>
        /// Nombre del sobre.
        /// </summary>
        [MaxLength(255)]
        public string DocumentSetName { get; set; }

        /// <summary>
        /// Descripción del sobre.
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// Referencia externa de sobre para cliente.
        /// </summary>
        [MaxLength(64)]
        public string Reference { get; set; }

        /// <summary>
        /// Identificador único de equipo.
        /// </summary>
        [MaxLength(80)]
        public string TeamId { get; set; }
    }
}