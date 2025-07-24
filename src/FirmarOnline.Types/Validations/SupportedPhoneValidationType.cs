using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace FirmarOnline.Types.Validations
{
    /// <summary>
    /// Tipo para validar teléfonos soportados por la aplicación
    /// </summary>
    public class SupportedPhoneValidationType : StringValidationTypeBase
    {
        private static readonly string[] _suportedPrefixes = new[]
        {
            CallingCodes.Andorra,
            CallingCodes.Argentina,
            CallingCodes.Belgium,
            CallingCodes.Bolivia,
            CallingCodes.Brazil,
            CallingCodes.Cameroon,
            CallingCodes.Chile,
            CallingCodes.Colombia,
            CallingCodes.CostaRica,
            CallingCodes.CoteIvoire,
            CallingCodes.Cuba,
            CallingCodes.Denmark,
            CallingCodes.Ecuador,
            CallingCodes.ElSalvador,
            CallingCodes.Finlandia,
            CallingCodes.France,
            CallingCodes.Germany,
            CallingCodes.Greece,
            CallingCodes.Guatemala,
            CallingCodes.Honduras,
            CallingCodes.Ireland,
            CallingCodes.Israel,
            CallingCodes.Italy,
            CallingCodes.Kuwait,
            CallingCodes.Luxembourg,
            CallingCodes.Mexico,
            CallingCodes.Morocco,
            CallingCodes.Netherlands,
            CallingCodes.Nicaragua,
            CallingCodes.NorthAmerica,
            CallingCodes.Norway,
            CallingCodes.Panama,
            CallingCodes.Paraguay,
            CallingCodes.Peru,
            CallingCodes.Poland,
            CallingCodes.Portugal,
            CallingCodes.PuertoRico,
            CallingCodes.Romania,
            CallingCodes.SaudiArabia,
            CallingCodes.Senegal,
            CallingCodes.Serbia,
            CallingCodes.Slovenia,
            CallingCodes.Spain,
            CallingCodes.Switzerland,
            CallingCodes.Tunisia,
            CallingCodes.UnitedArabEmirates,
            CallingCodes.UnitedKingdom,
            CallingCodes.Uruguay,
            CallingCodes.Venezuela
        };

        private static readonly string _spanishPhonePattern = @"^[+]34(?:6|7)[0-9]{8}$";

        /// <summary>
        /// Comprueba que el valor es un número de teléfono válido
        /// </summary>
        /// <param name="value">El valor a comprobar</param>
        /// <returns>true si es un valor válido, de lo contrario false</returns>
        public override bool IsValid(string value)
        {
            var phoneNumberValidation = new PhoneAttribute();
            if (!phoneNumberValidation.IsValid(value))
            {
                return false;
            }

            var prefix = GetPhoneNumberPrefix(value);
            return prefix switch
            {
                null => false,
                CallingCodes.Spain => Regex.IsMatch(value, _spanishPhonePattern),
                _ => true,
            };
        }

        /// <summary>
        /// Normaliza un número de teléfono español eliminando paréntesis
        /// y espacios
        /// </summary>
        /// <param name="value">El valor a normalizar</param>
        /// <returns>El número de teléfono normalizado</returns>
        public override string Normalize(string value)
        {
            return value?.Replace(" ", "").Replace("(", "").Replace(")", "");
        }

        private string GetPhoneNumberPrefix(string phoneNumber)
        {
            return _suportedPrefixes.Where(p => phoneNumber.StartsWith($"+{p}"))
                .OrderByDescending(p => p.Length).FirstOrDefault();
        }
    }
}