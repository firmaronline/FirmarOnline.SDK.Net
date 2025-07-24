using Edatalia.Types;
using FirmarOnline.Model;
using FirmarOnline.Model.PSC;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmarOnline.Clients.PSC
{
    public partial class PSCClient
    {
        /// <summary>
        /// Recupera un listado de los documentos enviados a firmar.
        /// Si se indica documentSetId, se omiten los valores del filtro.
        /// </summary>
        /// <param name="filter">Opciones de filtrado para aplicar al listado</param>
        /// <param name="documentSetId">Identificador único del sobre.</param>
        /// <returns>La lista de documentos</returns>
        public async Task<PageResult<DocumentSetSummary>> GetHistoryAsync(
            DocumentSetFilter filter = null, string documentSetId = null)
        {
            var requestUrl = "history";

            var query = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(documentSetId))
            {
                query.Add(new KeyValuePair<string, string>("DocumentSetId", documentSetId));
            }
            else
            {
                if (filter != null)
                {
                    if (filter.OnlyCurrentUser)
                    {
                        query.Add(new KeyValuePair<string, string>("OnlyCurrentUser", "true"));
                    }

                    if (filter.Status != null && filter.Status.Length > 0)
                    {
                        foreach (DocumentSetStatusCode documentSetStatusCode in filter.Status)
                        {
                            query.Add(new KeyValuePair<string, string>("status", documentSetStatusCode.ToString()));
                        }
                    }

                    if (filter.SendMethod != null && filter.SendMethod.Length != 0)
                    {
                        foreach (SendMethod sendMethod in filter.SendMethod)
                        {
                            query.Add(new KeyValuePair<string, string>("SendMethod", sendMethod.ToString()));
                        }
                    }

                    if (filter.RecipientActionTypes != null && filter.RecipientActionTypes.Length != 0)
                    {
                        foreach (RecipientActionType recipientActionType in filter.RecipientActionTypes)
                        {
                            query.Add(new KeyValuePair<string, string>("RecipientActionTypes", recipientActionType.ToString()));
                        }
                    }

                    if (!string.IsNullOrEmpty(filter.Reference))
                    {
                        query.Add(new KeyValuePair<string, string>("reference", filter.Reference));
                    }

                    if (!string.IsNullOrEmpty(filter.DocumentSetName))
                    {
                        query.Add(new KeyValuePair<string, string>("documentSetName", filter.DocumentSetName));
                    }

                    if (!string.IsNullOrEmpty(filter.RecipientName))
                    {
                        query.Add(new KeyValuePair<string, string>("recipientName", filter.RecipientName));
                    }

                    if (!string.IsNullOrEmpty(filter.RecipientEmail))
                    {
                        query.Add(new KeyValuePair<string, string>("recipientEmail", filter.RecipientEmail));
                    }

                    if (!string.IsNullOrEmpty(filter.RecipientPhoneNumber))
                    {
                        query.Add(new KeyValuePair<string, string>("recipientPhoneNumber", filter.RecipientPhoneNumber));
                    }

                    if (!string.IsNullOrEmpty(filter.RecipientCardId))
                    {
                        query.Add(new KeyValuePair<string, string>("recipientCardId", filter.RecipientCardId));
                    }

                    if (!string.IsNullOrEmpty(filter.DocumentName))
                    {
                        query.Add(new KeyValuePair<string, string>("documentName", filter.DocumentName));
                    }

                    if (filter.Teams != null && filter.Teams.Length > 0)
                    {
                        foreach (string team in filter.Teams)
                        {
                            query.Add(new KeyValuePair<string, string>("teams", team));
                        }
                    }

                    if (filter.Limit > 0)
                    {
                        query.Add(new KeyValuePair<string, string>("limit", filter.Limit.ToString()));
                    }

                    if (filter.Offset > 0)
                    {
                        query.Add(new KeyValuePair<string, string>("offset", filter.Offset.ToString()));
                    }

                    if (filter.FromDateTime.HasValue)
                    {
                        query.Add(new KeyValuePair<string, string>("fromDateTime", filter.FromDateTime.Value.ToString("O")));
                    }

                    if (filter.ToDateTime.HasValue)
                    {
                        query.Add(new KeyValuePair<string, string>("toDateTime", filter.ToDateTime.Value.ToString("O")));
                    }
                }
            }

            if (query.Count > 0)
            {
                var queryString = string.Join("&", query.Select(item => $"{item.Key}={item.Value}"));
                requestUrl = $"{requestUrl}?{queryString}";
            }

            var result = await GetPageAsync<DocumentSetSummary>(requestUrl, filter);

            CheckResponseStatus(result);

            return result.PageResult;
        }
    }
}