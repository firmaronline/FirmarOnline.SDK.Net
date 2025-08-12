using FirmarOnline.Model.PSC;
using System.Threading.Tasks;

namespace FirmarOnline.Clients.PSC
{
    public partial class PSCClient
    {
        /// <summary>
        /// Crea un nuevo sobre a partir de un flujo, pudiendo informar valores
        /// </summary>
        /// <param name="documentSetFlow">Definición del sobre creado a partir de un flujo</param>
        /// <returns>Identificador único del sobre creado</returns>
        public async Task<string> PostDocumentSetFlowAsync(DocumentSetFlow documentSetFlow)
        {
            var result = await PostAsync<DocumentSetFlow, string>("v40/documentset/flow", documentSetFlow);

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Crea un nuevo sobre y devuelve la url de acceso al visor
        /// </summary>
        /// <param name="documentSetFlowUrl">Definición del sobre creado a partir de un flujo</param>
        /// <returns>Un objeto <see cref="NewDocumentSet"/> con el identificador único del sobre
        /// y la url de acceso al visor</returns>
        public async Task<NewDocumentSet> PostDocumentSetFlowAndGetUrlAsync(DocumentSetFlowUrlWithOverrides documentSetFlowUrl)
        {
            var result = await PostAsync<DocumentSetFlowUrlWithOverrides, NewDocumentSet>("v40/documentset/flow/url", documentSetFlowUrl);

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Crea un nuevo sobre a partir de un flujo
        /// </summary>
        /// <param name="documentSetFlow">Definición del sobre creado a partir de un flujo</param>
        /// <returns>Identificador único del sobre creado</returns>
        public async Task<string> PostDocumentSetFlowSimpleAsync(DocumentSetFlow documentSetFlow)
        {
            var result = await PostAsync<DocumentSetFlow, string>("v40/documentset/flow/simple", documentSetFlow);

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Crea un nuevo sobre
        /// </summary>
        /// <param name="documentSet">Definición del sobre</param>
        /// <returns>Identificador único del sobre creado</returns>
        public async Task<string> PostDocumentSetAsync(DocumentSet documentSet)
        {
            var result = await PostAsync<DocumentSet, string>("v40/documentset", documentSet);

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Crea un nuevo sobre y devuelve la url de acceso al visor
        /// </summary>
        /// <param name="documentSet">Definición del sobre creado</param>
        /// <returns>Un objeto <see cref="NewDocumentSet"/> con el identificador único del sobre
        /// y la url de acceso al visor</returns>
        public async Task<NewDocumentSet> PostDocumentSetAndGetUrlAsync(SimpleDocumentSet documentSet)
        {
            var result = await PostAsync<SimpleDocumentSet, NewDocumentSet>("v40/documentset/url", documentSet);

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Crea un sobre con un único documento y destinatario indicando el método de envío de las urls
        /// </summary>
        /// <param name="documentSet">Definición del sobre</param>
        /// <returns>Identificador único del sobre creado</returns>
        public async Task<string> PostDocumentSetSimpleAsync(SimpleDocumentSetWithSendMethod documentSet)
        {
            var result = await PostAsync<SimpleDocumentSetWithSendMethod, string>("v40/documentset/simple", documentSet);

            CheckResponseStatus(result);
            return result.Value;
        }

    }
}
