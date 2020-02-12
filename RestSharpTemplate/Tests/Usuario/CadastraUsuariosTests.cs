using DesafioAPI.Bases;
using NUnit.Framework;
using RestSharp;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using System.Collections.Generic;

namespace DesafioAPI.Tests.Usuario
{
    [TestFixture]
    public class CadastraUsuariosTests : TestBase
    {
        CadastraProjetoRequests cadastraProjetoRequests = new CadastraProjetoRequests();
        CadastraUsuarioRequest cadastraUsuarioRequest = new CadastraUsuarioRequest();

        [Test]
        public void CadastrarUsuario()
        {
            #region Parameters Cadastro Tarefa
            string nome = "nome user";
            string nomeReal = "nome real";
            string email = "erika@gmail.com";
            string senha = "administrator";
            string projeto = "projeto geral";
            string statusCodeEsperado = "Created";
            #endregion
            VerificaProjetoExiste(projeto);
            VerificaUsuarioExiste(nome);

            cadastraUsuarioRequest.SetJsonBody(nome, senha, nomeReal, email);
            IRestResponse<dynamic> response = cadastraUsuarioRequest.ExecuteRequest();

            string retornoUserName = response.Data["user"]["name"];
            string retornoRealName = response.Data["user"]["real_name"];
            string retornoEmail = response.Data["user"]["email"];
            string retornoProjeto = response.Data["user"]["projects"][0]["name"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(nome, retornoUserName);
                Assert.AreEqual(nomeReal, retornoRealName);
                Assert.AreEqual(email, retornoEmail);
                Assert.AreEqual(projeto, retornoProjeto);
            });
        }

        public void VerificaProjetoExiste(string nomeProjeto)
        {
            if (ProjetoDBSteps.VerificaProjetoExiste(nomeProjeto).Equals(0))
            {
                cadastraProjetoRequests.SetJsonBody(nomeProjeto, "");
                cadastraProjetoRequests.ExecuteRequest();
            }
        }

        public void VerificaUsuarioExiste(string nomeUsuario)
        {
            if (!UsuarioDBSteps.VerificaUsuarioExiste(nomeUsuario).Count.Equals(0))
            {
                UsuarioDBSteps.DeletaUsuario(nomeUsuario);
            }
        }
    }
}