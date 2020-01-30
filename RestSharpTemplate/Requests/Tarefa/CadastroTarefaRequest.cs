using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Requests.Tarefas
{
    public class CadastroTarefaRequest : RequestBase
    {
        public CadastroTarefaRequest()
        {
            requestService = "/api/rest/issues";
            method = Method.POST;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }
        public void SetJsonBody(string resumo, string descricao, string informacao, string projeto,
                               string categoria, 
                               string visibilidade,
                               string prioridade,
                               string severidade,
                               string reprodutibilidade,
                               string tarefa,
                               string tag)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Tarefa/CadastroTarefaJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$resumo", resumo);
            jsonBody = jsonBody.Replace("$descricao", descricao);
            jsonBody = jsonBody.Replace("$informacao", informacao);
            jsonBody = jsonBody.Replace("$projeto", projeto);
            jsonBody = jsonBody.Replace("$categoria", categoria);
            jsonBody = jsonBody.Replace("$user", categoria);
            jsonBody = jsonBody.Replace("$visibilidade", visibilidade);
            jsonBody = jsonBody.Replace("$prioridade", prioridade);
            jsonBody = jsonBody.Replace("$severidade", severidade);
            jsonBody = jsonBody.Replace("$reprodutibilidade", reprodutibilidade);
            jsonBody = jsonBody.Replace("$tarefa", tarefa);
            jsonBody = jsonBody.Replace("$tag", tag);
        }
    }
}
