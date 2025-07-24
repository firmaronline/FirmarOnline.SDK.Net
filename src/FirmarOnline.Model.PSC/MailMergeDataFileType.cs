using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Tipos de ficheros de datos para Combinación de correspondencia.
    /// </summary>
    public enum MailMergeDataFileType
    {
        /// <summary>
        /// Fichero de datos de tipo Excel.
        /// </summary>
        [Display(Name = "Excel")]
        Excel = 0,

        /// <summary>
        /// Fichero de datos de tipo CSV.
        /// </summary>
        [Display(Name = "CSV")]
        CSV = 1
    }

    /// <summary>
    /// Métodos de extensión para <see cref="MailMergeDataFileType"/>
    /// </summary>
    public static class MailMergeDataFileTypeExtensions
    {
        /// <summary>
        /// Obtiene la extensión de un tipo de fichero de datos de la combinación de correspondecia
        /// </summary>
        /// <param name="fileType">Tipo de ficehro</param>
        /// <returns></returns>
        public static string GetFileExtension(this MailMergeDataFileType fileType)
        {
            return fileType switch
            {
                MailMergeDataFileType.CSV => "csv",
                MailMergeDataFileType.Excel => "xlsx",
                _ => "Tipo desconocido"
            };
        }
    }
}