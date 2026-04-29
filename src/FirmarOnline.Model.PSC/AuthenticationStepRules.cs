using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Reglas comunes de validación para la propiedad AuthSteps de un destinatario o flujo.
    /// </summary>
    internal static class AuthenticationStepRules
    {
        private const string AuthStepsName = "AuthSteps";
        private const string AccessCodeName = "AccessCode";

        /// <summary>
        /// Valida la secuencia de pasos de autenticación cuando se utiliza autenticación MFA.
        /// Comprueba que haya al menos dos pasos, que no haya pasos con tipos duplicados,
        /// y que, si existe un paso con tipo AccessCode, su Challenge esté informado.
        /// </summary>
        /// <param name="authSteps">Secuencia de pasos de autenticación.</param>
        /// <returns>Un <see cref="ValidationResult"/> con el error encontrado, o <see langword="null"/> si la validación es correcta.</returns>
        public static ValidationResult ValidateMfaSteps(AuthenticationStep[] authSteps)
        {
            if ((authSteps?.Length ?? 0) < 2)
            {
                return new ValidationResult("MFA authentication requires at least two AuthSteps.", [AuthStepsName]);
            }

            if (authSteps.GroupBy(s => s.Type).Any(g => g.Count() > 1))
            {
                return new ValidationResult("AuthSteps cannot contain duplicated Type values.", [AuthStepsName]);
            }

            var accessCodeStep = authSteps.FirstOrDefault(s => s.Type == RecipientAuthenticationType.AccessCode);
            return accessCodeStep != null
                ? ValidateAccessCodeChallenge(accessCodeStep.AccessCode?.Challenge)
                : null;
        }

        /// <summary>
        /// Valida que el Challenge de un código de acceso esté informado.
        /// </summary>
        /// <param name="challenge">Valor del Challenge del código de acceso.</param>
        /// <returns>Un <see cref="ValidationResult"/> con el error encontrado, o <see langword="null"/> si la validación es correcta.</returns>
        public static ValidationResult ValidateAccessCodeChallenge(string challenge)
        {
            return string.IsNullOrEmpty(challenge)
                ? new ValidationResult("The access code challenge data is required.", [AccessCodeName])
                : null;
        }

        /// <summary>
        /// Valida que no se hayan definido AuthSteps cuando el tipo de autenticación no es MFA.
        /// </summary>
        /// <param name="authSteps">Secuencia de pasos de autenticación.</param>
        /// <param name="authTypePropertyName">Nombre de la propiedad de tipo de autenticación (p.ej. "AuthType" o "AuthenticationType") para componer el mensaje de error.</param>
        /// <returns>Un <see cref="ValidationResult"/> con el error encontrado, o <see langword="null"/> si la validación es correcta.</returns>
        public static ValidationResult ValidateNoAuthSteps(AuthenticationStep[] authSteps, string authTypePropertyName)
        {
            return (authSteps?.Length ?? 0) > 0
                ? new ValidationResult($"AuthSteps can only be set when {authTypePropertyName} is MFA.", [AuthStepsName])
                : null;
        }
    }
}
