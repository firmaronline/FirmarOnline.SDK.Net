using Edatalia.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Clase base para definir destinatarios de un sobre de firma remota
    /// </summary>
    [CustomValidation(typeof(RecipientBase), nameof(ValidateRecipientBase),
        ErrorMessage = "The Recipient is not valid.")]
    public abstract class RecipientBase
    {
        /// <summary>
        /// Nombre
        /// </summary>
        [Required]
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
        [MaxLength(20)]
        public string CardId { get; set; }

        // TODO: En PhoneNumber, al pasar a .NETCore (no .netstandard2.0) hay que añadir un
        // DataAnnotation para indicar que se el valor puede ser null [AllowNull], y añadir al
        // DataAnnotation de StringLength la propiedad MinimumLength con valor 5. Tambíen habrá que
        // quitar la parte (o toda) la CustomValidation porque será redundante con los nuevos
        // elementos de DataAnnotations.
        /// <summary>
        /// Número de teléfono.
        /// </summary>
        [SupportedPhone]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Identificador de dispositivo.
        /// </summary>
        public Guid? DeviceId { get; set; }

        /// <summary>
        /// Código de acceso (para autenticación mediante código de acceso)
        /// </summary>
        public RecipientAccessCode AccessCode { get; set; }

        /// <summary>
        /// Definición de anexos
        /// </summary>
        public IEnumerable<RecipientDefinitionAttachment> Attachments { get; set; }

        /// <summary>
        /// Validación de destinatario.
        /// Si tiene número de teléfono, tiene que tener 5 caracteres como mínimo.
        /// </summary>
        /// <param name="recipientBase">Destinatario.</param>
        /// <returns></returns>
        public static ValidationResult ValidateRecipientBase(RecipientBase recipientBase)
        {
            if (!string.IsNullOrEmpty(recipientBase.PhoneNumber) && recipientBase.PhoneNumber.Length < 5)
            {
                return new ValidationResult("The field PhoneNumber must be a string or array type with a minimum length of '5'.");
            }

            return ValidationResult.Success;
        }
    }
}