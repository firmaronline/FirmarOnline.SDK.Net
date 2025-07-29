namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// datos de GPS
    /// </summary>
    public class GPSData
    {
        /// <summary>
        /// GPS válido?
        /// </summary>
        public bool GPSValid { get; set; }

        /// <summary>
        /// datos de GPS sin parsear
        /// </summary>
        public string GPSBulk { get; set; }

        /// <summary>
        /// latitud GPS
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// latitud GPS
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// azimut GPS
        /// </summary>
        public double Azimuth { get; set; }
        /// <summary>
        /// precisión GPS
        /// </summary>
        public double Precission { get; set; }
    }
}