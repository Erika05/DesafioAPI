using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System.IO;
using System.Text;

namespace DesafioAPI.Requests.Tarefas
{
    public class DeletaNotaTarefaRequest : RequestBase
    {
        public DeletaNotaTarefaRequest()
        {
            requestService = "/api/rest/issues/{issue_id}/notes/{issue_note_id}";
            method = Method.DELETE;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idTarefa, string idNota)
        {
            parameters.Clear();
            parameters.Add("issue_id", idTarefa);
            parameters.Add("issue_note_id", idNota);
        }

        public void SetJsonBody(string nota, string status)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Tarefa/DeletaNotaTarefaJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$nota", nota);
            jsonBody = jsonBody.Replace("$status", status);
        }
    }
}
