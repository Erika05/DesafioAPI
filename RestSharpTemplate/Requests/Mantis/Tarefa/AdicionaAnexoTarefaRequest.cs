using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System.IO;
using System.Text;

namespace DesafioAPI.Requests.Tarefas
{
    public class AdicionaAnexoTarefaRequest : RequestBase
    {
        public AdicionaAnexoTarefaRequest()
        {
            requestService = "/api/rest/issues/{issue_id}/files";
            method = Method.POST;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idTarefa)
        {
            parameters.Clear();
            parameters.Add("issue_id", idTarefa);
        }

        public void SetJsonBody(string nomeAnexo,
                              string anexo)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Mantis/Tarefa/AdicionaAnexoTarefaJson.json", Encoding.UTF8);
         
            jsonBody = jsonBody.Replace("$nomeAnexo", nomeAnexo);
            jsonBody = jsonBody.Replace("$anexo", anexo);
        }
    }
}
