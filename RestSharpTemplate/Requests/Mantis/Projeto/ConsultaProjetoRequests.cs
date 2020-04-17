using DesafioAPI.Bases;
using RestSharp;

namespace DesafioAPI.Requests.Projeto
{
    public class ConsultaProjetoRequests : RequestBase
    {
        public ConsultaProjetoRequests()
        {
            requestService = "/api/rest/projects/{project_id}";
            method = Method.GET;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }

        public void SetParameters(string idProjeto)
        {
            parameters.Clear();
            parameters.Add("project_id", idProjeto);
        }
    }
}
