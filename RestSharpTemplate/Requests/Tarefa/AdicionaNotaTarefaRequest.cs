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
            parameters.Clear();
            parameters.Add("issue_id", idTarefa);
        }

        public void SetJsonBody(string nota, string status)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Tarefa/AdicionaNotaTarefaJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$nota", nota);
            jsonBody = jsonBody.Replace("$status", status);
        }

        public void SetJsonBody(string nota, string status, string duracao)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Tarefa/AdicionaNotaComTempoTarefaJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$nota", nota);
            jsonBody = jsonBody.Replace("$status", status);
            jsonBody = jsonBody.Replace("$duracao", duracao);
        }

        public void SetJsonBody(string nota, string status, string duracao, string nomeAnexo, string anexo)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Tarefa/AdicionaNotaComAnexoTarefaJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$nota", nota);
            jsonBody = jsonBody.Replace("$status", status);
            jsonBody = jsonBody.Replace("$duracao", duracao);
            jsonBody = jsonBody.Replace("$nomeAnexo", nomeAnexo);
            jsonBody = jsonBody.Replace("$anexo", anexo);
        }

    }
}
