using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Tarefas;
using RestSharp;
using System.Collections.Generic;

namespace DesafioAPI.Tests.Usuario
{
    public class HelpersUsuarios : TestBase
    {
        CadastraUsuarioRequest cadastraUsuarioRequest = new CadastraUsuarioRequest();

        public string PreparaBaseCadastradoUsuario(string nomeUsuario, string senha, string nomeReal, string email)
        {
            string idUsuario = "";
            List<string> dadosUsuario = UsuarioDBSteps.VerificaUsuarioExiste(nomeUsuario);
            if (dadosUsuario.Count.Equals(0))
            {
                cadastraUsuarioRequest.SetJsonBody(nomeUsuario, senha, nomeReal, email);
                IRestResponse<dynamic> response = cadastraUsuarioRequest.ExecuteRequest();
                idUsuario = response.Data["user"]["id"];
            }
            else
            {

                idUsuario = dadosUsuario[0];
            }
            return idUsuario;
        }

        public void PreparaBaseDeletadoUsuario(string nomeUsuario)
        {
            if (!UsuarioDBSteps.VerificaUsuarioExiste(nomeUsuario).Count.Equals(0))
            {
                UsuarioDBSteps.DeletaUsuario(nomeUsuario);
            }
        }
    }
}
