#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.Forms
{
    /// <summary>
    /// Opciones de las listas desplegables
    /// </summary>
    public class DataSetOption
    {
        /// <summary>
        /// Clave 
        /// </summary>
        [MaxLength(50)]
        public string Key { get; set; }

        /// <summary>
        /// Lista de valores
        /// </summary>       
        public List<DataSetValue> Values { get; set; }

        /// <summary>
        /// Url con el origen de datos
        /// </summary>
        public string Ref { get; set; }
    }

    /// <summary>
    /// Opción de una lista
    /// </summary>
    public class DataSetValue
    {
        /// <summary>
        /// Valor de la opción.
        /// </summary>
        [MaxLength(50)]
        public string Value { get; set; }

        /// <summary>
        /// Texto de la opción.
        /// </summary>
        [MaxLength(255)]
        public string Text { get; set; }
    }
}
#endif