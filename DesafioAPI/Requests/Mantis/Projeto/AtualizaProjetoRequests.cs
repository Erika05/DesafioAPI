using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System.IO;
using System.Text;

namespace DesafioAPI.Requests.Projeto
{
    public class AtualizaProjetoRequests : RequestBase
    {
        public AtualizaProjetoRequests()
        {
            requestService = "/api/rest/projects/{project_id}";
            method = Method.PATCH;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idProjeto)
        {
            parameters.Add("project_id", idProjeto);
        }

        public void SetJsonBody(string name,
                              string idProjeto)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Mantis/Projeto/AtualizaProjetoJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$nomeProjeto", name);
            jsonBody = jsonBody.Replace("$id", idProjeto);
        }
    }
}
