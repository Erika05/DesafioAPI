using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Tarefa
{
    public class AtualizaTarefaRequest : RequestBase
    {
        public AtualizaTarefaRequest()
        {
            requestService = "/api/rest/issues/{issue_id}";
            method = Method.PATCH;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }
              
        public void SetParameters(string idTarefa)
        {
            parameters.Clear();
            parameters.Add("issue_id", idTarefa);
        }

        public void SetJsonBody(string status)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Mantis/Tarefa/AtualizaTarefaMinimalJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$status", status);
        }

        public void SetJsonBody(string resumo, string prioridade, string status)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Mantis/Tarefa/AtualizaTarefaJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$resumo", resumo);
            jsonBody = jsonBody.Replace("$prioridade", prioridade);
            jsonBody = jsonBody.Replace("$status", status);
        }

    }
}
