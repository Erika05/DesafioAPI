using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System.IO;
using System.Text;

namespace DesafioAPI.Requests.Tarefas
{
    public class AdicionaMonitoramentoTarefaRequest : RequestBase
    {
        public AdicionaMonitoramentoTarefaRequest()
        {
            requestService = "/api/rest/issues/{issue_id}/monitors";
            method = Method.POST;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idTarefa)
        {
            parameters.Add("issue_id", idTarefa);
        }

        public void SetJsonBody(string userName,
                             string userRealname)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Tarefa/AdicionaMonitoramentoUsuarioTarefaJson.json", Encoding.UTF8);

            jsonBody = jsonBody.Replace("$userName", userName);
            jsonBody = jsonBody.Replace("$userRealname", userRealname);
        }

    }
}
