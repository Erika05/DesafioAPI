using DesafioAPI.Bases;
using DesafioAPI.Helpers;
using RestSharp;
using System.IO;
using System.Text;

namespace DesafioAPI.Requests.Tarefas
{
    public class CadastraUsuarioRequest : RequestBase
    {
        public CadastraUsuarioRequest()
        {
            requestService = "/api/rest/users/";
            method = Method.POST;
            headers.Add("Authorization", Properties.Settings.Default.TOKEN);
        }
        public void SetJsonBody(string nome,
                               string senha,
                               string nomeReal,
                               string email)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Usuario/CadastraUsuarioJson.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$nome", nome);
            jsonBody = jsonBody.Replace("$senha", senha);
            jsonBody = jsonBody.Replace("$realNome", nomeReal);
            jsonBody = jsonBody.Replace("$email", email);
        }
    }
}
