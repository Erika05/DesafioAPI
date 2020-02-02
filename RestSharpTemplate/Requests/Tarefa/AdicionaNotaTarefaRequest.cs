using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System.IO;
using System.Text;

namespace DesafioAPI.Requests.Tarefas
{
    public class AdicionaNotaTarefaRequest : RequestBase
    {
        public AdicionaNotaTarefaRequest()
        {
            requestService = "/api/rest/issues/{issue_id}/notes";
            method = Method.POST;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idTarefa)
        {
            parameters.Add("issue_id", idTarefa);
        }

        public void SetJsonBody(string nota,
                              string status)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Tarefa/AdicionaNotaTarefaJson.json", Encoding.UTF8);
         
            jsonBody = jsonBody.Replace("$nota", nota);
            jsonBody = jsonBody.Replace("$status", status);
        }
    }
}
