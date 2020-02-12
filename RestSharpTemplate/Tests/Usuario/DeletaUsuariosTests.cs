using DesafioAPI.Bases;
using NUnit.Framework;
using RestSharp;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;

namespace DesafioAPI.Tests.Usuario
{
    [TestFixture]
    public class DeletaUsuariosTests : TestBase
    {
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();
        CadastraUsuarioRequest cadastraUsuarioRequest = new CadastraUsuarioRequest();

        [Test]
        public void DeletarUsuario()
        {
            #region Parameters Cadastro Tarefa
            string nome = "nome user";
            string nomeReal = "nome real user";
            string email = "erika@gmail.com";
            string senha = "administrator";
            string projeto = "projeto geral";
            string statusCodeEsperado = "Created";
            #endregion
            VerificaProjetoExiste(projeto);
            cadastraUsuarioRequest.SetJsonBody(nome, senha, nomeReal, email);

            IRestResponse<dynamic> response = cadastraUsuarioRequest.ExecuteRequest();

            string retornoUserName = response.Data["user"][0]["name"];
            string retornoRealName = response.Data["user"][0]["real_name"];
            string retornoEmail = response.Data["user"][0]["email"];
            string retornoProjeto = response.Data["user"][0]["projects"][0]["name"];

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
                cadastroProjetoRequests.SetJsonBody(nomeProjeto, "");
                cadastroProjetoRequests.ExecuteRequest();
            }
        }
    }
}