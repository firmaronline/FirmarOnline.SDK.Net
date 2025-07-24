using System;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Información de par recipient-documento
    /// </summary>
    public class RecipientActionInfo
    {
        /// <summary>
        /// Tipo de RecipientAction
        /// </summary>
        public RecipientActionType ActionType { get; set; }

        /// <summary>
        /// Identificador de cliente del documento
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Estado del RecipientAction
        /// </summary>
        public RecipientActionStatusCode ActionStatus { get; set; }

        /// <summary>
        /// Instante actualización estado RecipientAction
        /// </summary>
        public DateTime? StatusTime { get; set; }
    }
}
