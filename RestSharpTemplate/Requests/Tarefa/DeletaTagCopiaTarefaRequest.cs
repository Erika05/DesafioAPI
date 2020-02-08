using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System.IO;
using System.Text;

namespace DesafioAPI.Requests.Tarefas
{
    public class DeletaTagCopiaTarefaRequest : RequestBase
    {
        public DeletaTagCopiaTarefaRequest()
        {
            requestService = "/api/rest/issues/{issue_id}/relationships/{relationship_id}";
            method = Method.DELETE;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idTarefa, string idRelacao)
        {
            parameters.Add("issue_id", idTarefa);
            parameters.Add("relationship_id", idRelacao);
        }

    }
}
