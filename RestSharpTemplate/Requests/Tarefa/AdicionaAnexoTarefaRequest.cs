﻿using DesafioAPI.Bases;
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
            parameters.Add("issue_id", idTarefa);
        }

        public void SetJsonBody(string nota,
                              string status)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Tarefa/AdicionaAnexoTarefaJson.json", Encoding.UTF8);
         
            jsonBody = jsonBody.Replace("$nomeAnexo", nota);
            jsonBody = jsonBody.Replace("$anexo", status);
        }
    }
}