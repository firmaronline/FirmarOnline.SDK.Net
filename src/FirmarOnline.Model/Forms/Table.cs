#if NET6_0_OR_GREATER

using System.Collections.Generic;

namespace FirmarOnline.Model.Forms
{
    /// <summary>
    /// Tabla.
    /// </summary>
    public class Table : VisibleItemBase
    {
        /// <summary>
        /// Celdas de la tabla.
        /// </summary>
        public Cells Cells { get; set; }
    }

    /// <summary>
    /// Celdas.
    /// </summary>
    public class Cells
    {
        /// <summary>
        /// Cabeceras.
        /// </summary>
        public List<string> Th { get; set; }

        /// <summary>
        /// Filas.
        /// </summary>
        public List<Tds> Tr { get; set; }
    }

    /// <summary>
    /// Columnas.
    /// </summary>
    public class Tds
    {
        /// <summary>
        /// Lista de columnas.
        /// </summary>
        public List<string> Td { get; set; }
    }
}

#endif