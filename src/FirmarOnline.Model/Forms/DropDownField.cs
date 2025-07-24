#if NET6_0_OR_GREATER
using System.Collections.Generic;

namespace FirmarOnline.Model.Forms
{
    /// <summary>
    /// Campo de tipo lista desplegable.
    /// </summary>
    public class DropDownField : InputItemBase
    {
        /// <summary>
        /// Origen de los datos, sera una key de los DataSets.
        /// </summary>
        public string DataSet { get; set; }

        /// <summary>
        /// Búsqueda (false por defecto).
        /// </summary>
        public bool Search { get; set; } = false;

        /// <summary>
        /// Ejemplo de lo que se espera que el usuario ingrese
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Valor por defecto de la lista de valores cargados.
        /// </summary>
        public string Checked { get; set; }

        /// <summary>
        /// Acciones en caso de selección.
        /// </summary>
        public List<Action> Actions { get; set; }
    }
}
#endif