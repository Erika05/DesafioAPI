using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System.IO;
using System.Text;

namespace DesafioAPI.Requests.Tarefas
{
    public class AdicionaTagCopiaTarefaRequest : RequestBase
    {
        public AdicionaTagCopiaTarefaRequest()
        {
            requestService = "/api/rest/issues/{issue_id}/relationships/";
            method = Method.POST;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idTarefa)
        {
            parameters.Add("issue_id", idTarefa);
        }

        public void SetJsonBody(string idTarefa)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Tarefa/AdicionaTagCopiaTarefaJson.json", Encoding.UTF8);

            jsonBody = jsonBody.Replace("$idTarefa", idTarefa);
        }

    }
}
