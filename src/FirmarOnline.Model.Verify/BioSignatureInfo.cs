using System;
using System.Collections.Generic;
using System.Drawing;

namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Información de verificación de una firma biométrica
    /// </summary>
    public class BioSignatureInfo
    {
        /// <summary>
        /// La firma tiene datos biométricos?
        /// </summary>
        public bool HasBioData { get; set; }
        /// <summary>
        /// Y son válidos?
        /// </summary>
        public bool BioDataValid { get; set; }
        /// <summary>
        /// indica si se han descifrado o no los datos biométricos
        /// </summary>
        public bool Decrypted { get; set; }

        /// <summary>
        /// datos biométricos en ISO o no
        /// </summary>
        public bool IsISO { get; set; }

        /// <summary>
        /// versión de datos biométricos. 0: sin datos biométricos; 1: sólo parte privada; 2: parte pública + privada
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// índice real de la firma
        /// </summary>
        public int EffectiveIndex { get; set; }

        /// <summary>
        /// Máximo de puntos en eje X
        /// </summary>
        public int Max_X { get; set; }
        /// <summary>
        /// Máximo de puntos en eje Y
        /// </summary>
        public int Max_Y { get; set; }
        /// <summary>
        /// Máximo de presión
        /// </summary>
        public int Max_Press { get; set; }
        /// <summary>
        /// Anchura de la pantalla de captura
        /// </summary>
        public int LCD_Width { get; set; }
        /// <summary>
        /// Altura de la pantalla de captura
        /// </summary>
        public int LCD_Height { get; set; }

        /// <summary>
        /// Punto arriba a la izda de zona de captura de firma
        /// </summary>
        public Point CaptureAreaTopLeft { get; set; }

        /// <summary>
        /// Punto abajo a la derecha de zona de captura de firma
        /// </summary>
        public Point CaptureAreaBottomRight { get; set; }

        /// <summary>
        /// timestamp de inicio de firma
        /// </summary>
        public DateTime SignatureStart { get; set; }
        /// <summary>
        /// timestamp de fin de firma
        /// </summary>
        public DateTime SignatureEnd { get; set; }

        /// <summary>
        /// comentario de la firma (si se ha puesto)
        /// </summary>
        public string SignatureComment { get; set; }

        /// <summary>
        /// Enumeración de tipo de datos incluídos en la cabecera de los datos bio
        /// </summary>
        public BiometricHeaderData IncludedData { get; set; }

        /// <summary>
        /// información de hardware
        /// </summary>
        public List<string> HardwareData { get; set; }

        /// <summary>
        /// información de software
        /// </summary>
        public List<string> SoftwareData { get; set; }

        /// <summary>
        /// variables mostradas al usuario
        /// </summary>
        public List<string> VarsData { get; set; }

        /// <summary>
        /// datos de GPS
        /// </summary>
        public GPSData GPSData { get; set; }

        /// <summary>
        /// número de serie del certificado usado para cifrar los datos bio
        /// </summary>
        public string CertEncSerialNumber { get; set; }
        /// <summary>
        /// información del emisor del certificado usado para cifrar los datos bio
        /// </summary>
        public string CertEncIssuerInfo { get; set; }

        /// <summary>
        /// algoritmo de cifrado usado
        /// </summary>
        public string AlgEnc { get; set; }

        /// <summary>
        /// imagen del widget que aparece en el PDF
        /// </summary>
        public string WidgetJPGB64 { get; set; }

        /// <summary>
        /// imágenes biométricas de la firma
        /// </summary>
        public BiometricSignatureImages BioImages { get; set; }

        /// <summary>
        /// ratio de la caja de firma
        /// </summary>
        public double SignatureBoxAspectRatio { get; set; }
        /// <summary>
        /// presión máxima en los datos biométricos
        /// </summary>
        public int BioMax_Press { get; set; }
        /// <summary>
        /// velocidad máxima en los datos biométricos
        /// </summary>
        public double BioMax_Vel { get; set; }
        /// <summary>
        /// aceleración máxima en los datos biométricos
        /// </summary>
        public double BioMax_Accel { get; set; }
        /// <summary>
        /// total de trazos en los datos biométricos
        /// </summary>
        public int BioTotalStrokes { get; set; }
        /// <summary>
        /// tiempo en el aire del lápiz de los datos biométricos (ms)
        /// </summary>
        public int BioAirTime { get; set; }
        /// <summary>
        /// tiempo en contacto del lápiz de los datos biométricos (ms)
        /// </summary>
        public int BioContactTime { get; set; }
        /// <summary>
        /// tiempo total de la firma de los datos biométricos (ms)
        /// </summary>
        public int BioTotalTime { get; set; }
        /// <summary>
        /// Porcentaje de puntos que caen en el cuadrante 1 (superior izda)
        /// </summary>
        public double BioQuadrantPerc1 { get; set; }
        /// <summary>
        /// Porcentaje de puntos que caen en el cuadrante 2 (superior dcha)
        /// </summary>
        public double BioQuadrantPerc2 { get; set; }
        /// <summary>
        /// Porcentaje de puntos que caen en el cuadrante 3 (inferior izda)
        /// </summary>
        public double BioQuadrantPerc3 { get; set; }
        /// <summary>
        /// Porcentaje de puntos que caen en el cuadrante 4 (inferior dcha)
        /// </summary>
        public double BioQuadrantPerc4 { get; set; }
    }
}