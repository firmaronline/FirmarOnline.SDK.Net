using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FirmarOnline.Model
{
    /// <summary>
    /// Idiomas
    /// </summary>
    public enum LanguageCode
    {
        /// <summary>
        /// Español (España)
        /// </summary>
        [Display(Name = "Español")]
        es_ES = 0,
        /// <summary>
        /// Inglés
        /// </summary>
        [Display(Name = "Inglés")]
        en_GB = 10,
        /// <summary>
        /// Francés
        /// </summary>
        [Display(Name = "Francés")]
        fr_FR = 20,
        /// <summary>
        /// Italiano
        /// </summary>
        [Display(Name = "Italiano")]
        it_IT = 30,
        /// <summary>
        /// Portugués
        /// </summary>
        [Display(Name = "Portugués")]
        pt_PT = 40,
        /// <summary>
        /// Euskera
        /// </summary>
        [Display(Name = "Euskera")]
        eu_ES = 200
    }

    /// <summary>
    /// Define métodos de extensión para la enumeración <see cref="LanguageCode"/>
    /// </summary>
    public static class LanguageCodeExtensions
    {
        /// <summary>
        /// Convierte enumeración <see cref="LanguageCode"/> a <see cref="CultureInfo"/>
        /// </summary>
        /// <param name="code">Objeto del tipo <see cref="LanguageCode"/></param>
        /// <returns>Objeto del tipo <see cref="CultureInfo"/></returns>
        public static CultureInfo GetCultureInfo(this LanguageCode code)
        {
            switch (code)
            {          
                case LanguageCode.en_GB:
                    return CultureInfo.CreateSpecificCulture("en-GB");
                case LanguageCode.fr_FR:
                    return CultureInfo.CreateSpecificCulture("fr-FR");
                case LanguageCode.it_IT:
                    return CultureInfo.CreateSpecificCulture("it-IT");
                case LanguageCode.pt_PT:
                    return CultureInfo.CreateSpecificCulture("pt-PT");
                case LanguageCode.eu_ES:
                    return CultureInfo.CreateSpecificCulture("eu-ES");
                case LanguageCode.es_ES:
                default:
                    return CultureInfo.CreateSpecificCulture("es-ES");
            }
        }
    }
}
