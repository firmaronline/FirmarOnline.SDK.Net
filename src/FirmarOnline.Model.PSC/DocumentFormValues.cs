using System.Collections.Generic;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Valores de formulario de un documento.
    /// </summary>
    public class DocumentFormValues
    {
        /// <summary>
        /// Identificador de documento.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Colección de valores de formulario.
        /// </summary>
        public ICollection<FormValue> FormValues { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Identificador de documento.</param>
        /// <param name="formValues">Colección de valores de formulario.</param>
        public DocumentFormValues(string id, ICollection<FormValue> formValues)
        {
            Id = id;
            FormValues = formValues;
        }
    }
}