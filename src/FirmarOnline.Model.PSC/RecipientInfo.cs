using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Información del destinatario.
    /// </summary>
    public class RecipientInfo
    {
        /// <summary>
        /// Identificador del cliente
        /// </summary>
        [MaxLength(120)]
        public string Recipientid { get; set; }

        /// <summary>
        /// Orden del recipient en el sobre.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Identificación (DNI, NIF,...)
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// Número de teléfono
        /// </summary>
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Tipo de autenticación que debe realizar el destinatario sobre los documentos
        /// </summary>
        public RecipientAuthenticationType AuthType { get; set; }

        /// <summary>
        /// Secuencia de pasos de autenticación (presente si AuthType = MFA)
        /// </summary>
        public AuthenticationStepInfo[] AuthSteps { get; set; }

        /// <summary>
        /// Acción que debe realizar el destinatario sobre los documentos
        /// </summary>
        public IEnumerable<RecipientActionInfo> RecipientActions { get; set; }

        /// <summary>
        /// Anexos
        /// </summary>
        public IEnumerable<AttachmentInfo> Attachments { get; set; }
    }
}