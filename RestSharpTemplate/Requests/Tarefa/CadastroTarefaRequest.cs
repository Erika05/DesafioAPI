using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System.IO;
using System.Text;

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
        public void SetJsonBody(string resumo,
                               string descricao,
                               string informacao, 
                               string projeto,
                               string categoria, 
                               string visibilidade,
                               string prioridade,
                               string severidade,
                               string reprodutibilidade,
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
            jsonBody = jsonBody.Replace("$tag", tag);
        }

        public void SetJsonBody(string resumo,
                              string descricao,
                              string categoria,
                              string projeto)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Tarefa/CadastroTarefaMinimalJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$resumo", resumo);
            jsonBody = jsonBody.Replace("$descricao", descricao);
            jsonBody = jsonBody.Replace("$categoria", categoria);
            jsonBody = jsonBody.Replace("$projeto", projeto);
        }

        public void SetJsonBody(string resumo,
                              string descricao,
                              string categoria,
                              string projeto,
                              string nomeAnexo,
                              string anexo)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Tarefa/CadastroTarefaAnexoJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$resumo", resumo);
            jsonBody = jsonBody.Replace("$descricao", descricao);
            jsonBody = jsonBody.Replace("$categoria", categoria);
            jsonBody = jsonBody.Replace("$projeto", projeto);
            jsonBody = jsonBody.Replace("$nomeAnexo", nomeAnexo);
            jsonBody = jsonBody.Replace("$anexo", anexo);
        }
    }
}
