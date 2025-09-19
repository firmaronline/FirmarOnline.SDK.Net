using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Lista tipada de destinatarios de un proceso de firma.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de destinatario que contiene la colección. Debe heredar de <see cref="RecipientBase"/>.
    /// </typeparam>
    [CustomValidation(typeof(RecipientCollectionValidators), nameof(RecipientCollectionValidators.ValidateOrderRequired), ErrorMessage = "The documentSet definition is not valid.")]
    [CustomValidation(typeof(RecipientCollectionValidators), nameof(RecipientCollectionValidators.ValidateParallelRecipientsWithActionType60), ErrorMessage = "The documentSet definition is not valid.")]
    public class RecipientCollection<T> : List<T> where T : RecipientBase { }

    /// <summary>
    /// Conjunto de validadores para <see cref="RecipientCollection{T}"/>.
    /// </summary>
    public static class RecipientCollectionValidators
    {
        /// <summary>
        /// Valida que, si algún destinatario tiene Order informado, entonces todos los destinatarios de la colección deben tener <c>Order</c> informado y distinto de 0.
        /// </summary>
        /// <param name="value">Colección a validar. Se espera un <see cref="IEnumerable{T}"/> de <see cref="Recipient"/> o <see cref="RecipientFlow"/>.</param>
        /// <param name="ctx">Contexto de validación utilizado para asociar el error a la propiedad decorada.</param>
        /// <returns>
        /// <see cref="ValidationResult.Success"/> si la colección cumple la regla; de lo contrario,
        /// un <see cref="ValidationResult"/> con el mensaje: You must indicate the order to all recipients."
        /// </returns>        
        public static ValidationResult ValidateOrderRequired(object value, ValidationContext ctx)
        {
            // Recipient
            if (value is IEnumerable<Recipient> rs)
            {
                var any = rs.Any(r => r.Order.HasValue);
                if (!any) return ValidationResult.Success;
                var bad = rs.Any(r => !r.Order.HasValue || r.Order == 0);
                return bad ? new ValidationResult("You must indicate the order to all recipients.", new[] { ctx?.MemberName ?? nameof(value) })
                           : ValidationResult.Success;
            }
            // RecipientFlow
            if (value is IEnumerable<RecipientFlow> fs)
            {
                var any = fs.Any(r => r.Order.HasValue);
                if (!any) return ValidationResult.Success;
                var bad = fs.Any(r => !r.Order.HasValue || r.Order == 0);
                return bad ? new ValidationResult("You must indicate the order to all recipients.", new[] { ctx?.MemberName ?? nameof(value) })
                           : ValidationResult.Success;
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Valida que no existan destinatarios en paralelo (mismo Order) con <see cref="RecipientActionType.CryptoAPISignature"/> (Action Type 60).
        /// </summary>
        /// <param name="value">Colección a validar. Se espera un <see cref="IEnumerable{T}"/> de <see cref="Recipient"/>.</param>
        /// <param name="ctx">Contexto de validación utilizado para asociar el error a la propiedad decorada.</param>
        /// <returns>
        /// <see cref="ValidationResult.Success"/> si no hay paralelismo con Action Type 60; en caso contrario,
        /// un <see cref="ValidationResult"/> con el mensaje: A document set cannot contain parallel recipients and Action Type 60.
        /// </returns>
        public static ValidationResult ValidateParallelRecipientsWithActionType60(object value, ValidationContext ctx)
        {
            if (value is IEnumerable<Recipient> rs)
            {
                var hasParallelCrypto = rs
                    .Where(r => r.ActionType == RecipientActionType.CryptoAPISignature && r.Order.HasValue)
                    .GroupBy(r => r.Order.Value)
                    .Any(g => g.Count() > 1);

                return hasParallelCrypto
                    ? new ValidationResult("A document set cannot contain parallel recipients and Action Type 60.", new[] { ctx?.MemberName ?? nameof(value) })
                    : ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}
