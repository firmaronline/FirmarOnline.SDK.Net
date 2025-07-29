namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Conjunto de imágenes generadas a partir de los datos biométricos de una firma electrónica.
    /// Cada imagen representa una visualización distinta de las características dinámicas capturadas durante el proceso de firma.
    /// Todas las imágenes están codificadas en Base64 en formato PNG.
    /// </summary>
    public class BiometricSignatureImages
    {
        /// <summary>
        /// Imagen del trazo de la firma sin información de presión, velocidad ni aceleración (trazo neutro).
        /// </summary>
        public string ImgBioNonePNGB64 { get; set; }

        /// <summary>
        /// Imagen con representación del trazo coloreado en función de la presión ejercida durante la firma.
        /// </summary>
        public string ImgBioPressurePNGB64 { get; set; }

        /// <summary>
        /// Imagen con representación del trazo coloreado en función de la velocidad del movimiento al firmar.
        /// </summary>
        public string ImgBioVelocityPNGB64 { get; set; }

        /// <summary>
        /// Imagen con representación del trazo coloreado en función de la aceleración durante la firma.
        /// </summary>
        public string ImgBioAccelerationPNGB64 { get; set; }

    }
}