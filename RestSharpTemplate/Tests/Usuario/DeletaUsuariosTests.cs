using DesafioAPI.Bases;
using NUnit.Framework;
using RestSharp;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using System.Collections.Generic;
using DesafioAPI.Requests.Usuario;

namespace DesafioAPI.Tests.Usuario
{
    [TestFixture]
    public class DeletaUsuariosTests : TestBase
    {
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();
        CadastraUsuarioRequest cadastraUsuarioRequest = new CadastraUsuarioRequest();
       
        string idUsuario = null;

        [Test]
        public void DeletarUsuario()
        {
            #region Parameters Cadastro Tarefa
            string nomeUsuario = "nome user";
            string nomeReal = "nome real user";
            string email = "erika@gmail.com";
            string senha = "administrator";
            string projeto = "projeto geral";
            string statusCodeEsperado = "NoContent";
            #endregion
            VerificaProjetoExiste(projeto);
            VerificaUsuarioExiste(nomeUsuario, senha, nomeReal, email);
            DeletaUsuarioRequest deletaUsuarioRequest = new DeletaUsuarioRequest(idUsuario);
            IRestResponse<dynamic> response = deletaUsuarioRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(string.Empty, UsuarioDBSteps.VerificaUsuarioExiste(nomeUsuario));
            });
        }

        public void VerificaProjetoExiste(string nomeProjeto)
        {
            if (ProjetoDBSteps.VerificaProjetoExiste(nomeProjeto).Equals(0))
            {
                cadastroProjetoRequests.SetJsonBody(nomeProjeto, "");
                cadastroProjetoRequests.ExecuteRequest();
            }
        }

        public void VerificaUsuarioExiste(string nomeUsuario, string senha, string nomeReal, string email)
        {
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
        }
    }
}