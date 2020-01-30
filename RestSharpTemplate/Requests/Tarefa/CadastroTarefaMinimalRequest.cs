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
    public class CadastroTarefaMinimalRequest : RequestBase
    {
        public CadastroTarefaMinimalRequest()
        {
            requestService = "/api/rest/issues";
            method = Method.POST;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
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
    }
}
