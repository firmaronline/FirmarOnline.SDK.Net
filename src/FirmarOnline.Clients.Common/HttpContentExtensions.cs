using System.Net.Http;
using System.Threading.Tasks;

#if NET6_0_OR_GREATER
using System.Net.Http.Json;
#else
using Newtonsoft.Json;
#endif

namespace FirmarOnline.Clients.Common
{
    /// <summary>
    /// Métodos de extensión para HttpContent
    /// </summary>
    public static class HttpContentExtensions
    {
        /// <summary>
        /// Lee el contenido HTTP y devuelve el valor resultante de la deserialización del contenido como JSON en una operación asíncrona
        /// aplicando las opciones de serialización por defecto de las APIs de Edatalia
        /// </summary>
        /// <typeparam name="T">Tipo de destino al que se va a deserializar</typeparam>
        /// <param name="content">El contenido del que se leerá</param>
        /// <returns>Objeto de tarea que representa la operación asíncrona</returns>
        public async static Task<T> ReadFromApiJsonAsync<T>(this HttpContent content)
        {
#if NET6_0_OR_GREATER
            return await content.ReadFromJsonAsync<T>(options: ApiDefaults.JsonSerializerOptions);
#else
            var json = await content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(json, ApiDefaults.JsonSerializerOptions);
#endif
        }
    }
}
