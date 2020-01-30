using DesafioAPI.Bases;
using RestSharp;

namespace DesafioAPI.Requests.Projeto
{
    public class ConsultaTodosProjetosRequest : RequestBase
    {
        public ConsultaTodosProjetosRequest()
        {
            requestService = "/api/rest/projects/";
            method = Method.GET;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }
    }
}
