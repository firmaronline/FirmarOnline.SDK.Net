using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Tipo de firma corporativa
    /// </summary>
    public enum CorporateSignatureType
    {
        /// <summary>
        /// None
        /// </summary>
        [Display(Name = "Ninguno")]
        None = 0,
        /// <summary>
        /// Inicio de sobre
        /// </summary>
        [Display(Name = "Inicio de sobre")]
        Start = 1,
        /// <summary>
        /// Final de sobre
        /// </summary>
        [Display(Name = "Final de sobre")]
        End = 2,
        /// <summary>
        /// Inicio y final de sobre
        /// </summary>
        [Display(Name = "Inicio y final de sobre")]
        StartAndEnd = 3
    }

    /// <summary>
    /// Define métodos de extensión para la enumeración <see cref="CorporateSignatureType"/>
    /// </summary>
    public static class CorporateSignatureTypeExtensions
    {
        private static readonly CorporateSignatureType[] _startType =
            [CorporateSignatureType.Start, CorporateSignatureType.StartAndEnd];

        private static readonly CorporateSignatureType[] _endType =
            [CorporateSignatureType.End, CorporateSignatureType.StartAndEnd];

        
        /// <summary>
        /// Comprueba si el tipo indica que hay firma corporativa al inicio de sobre
        /// </summary>
        /// <param name="type">Tipo a comprobar</param>
        /// <returns>true si tipo indica que hay firma corporativa al inicio de sobre,
        /// en otro caso devuelve false</returns>
        public static bool IsStartType(this CorporateSignatureType type)
        {
            return _startType.Contains(type);
        }

        /// <summary>
        /// Comprueba si el tipo indica que hay firma corporativa al final de sobre
        /// </summary>
        /// <param name="type">Tipo a comprobar</param>
        /// <returns>true si tipo indica que hay firma corporativa al final de sobre,
        /// en otro caso devuelve false</returns>
        public static bool IsEndType(this CorporateSignatureType type)
        {
            return _endType.Contains(type);
        }
    }
}
