using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Mantis.Projeto
{
    [TestFixture]
    public class CadastraProjetoTests : TestBase
    {
        CadastraProjetoRequests cadastraProjetoRequests = new CadastraProjetoRequests();
        HelpersProjetos helpersProjetos = new HelpersProjetos();

        [Test]
        public void CadastrarProjetoSucesso()
        {
            #region Parameters 
            string nomeProjeto = "projeto teste";
            string descricao = "projeto teste";
            string statusCodeEsperado = "Created";
            #endregion
            helpersProjetos.PreparaBaseDeletadoProjeto(nomeProjeto);
            cadastraProjetoRequests.SetJsonBody(nomeProjeto, descricao);
            IRestResponse<dynamic> response = cadastraProjetoRequests.ExecuteRequest();
            string retornoNomeProjetc = response.Data["project"]["name"];
            string retornoDescricaoProjetc = response.Data["project"]["description"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(nomeProjeto, retornoNomeProjetc);
                Assert.AreEqual(descricao, retornoDescricaoProjetc);
                Assert.AreEqual(1, ProjetoDBSteps.VerificaProjetoExiste(nomeProjeto));
            });
        }

        [Test]
        public void ProjetoJaCadastrado()
        {
            #region parameters 
            string nomeProjeto = "projeto ja cadastrado";
            string descricaoErro = "call to undefined method restfault::getmessage() in ";
            string statuscodeesperado = "OK";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(nomeProjeto);
            cadastraProjetoRequests.SetJsonBody(nomeProjeto, descricaoErro);
            IRestResponse<dynamic> response = cadastraProjetoRequests.ExecuteRequest();
            string body = response.Content;
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statuscodeesperado, response.StatusCode.ToString());
                Assert.That(true, descricaoErro, response.Content);
                Assert.AreEqual(1, ProjetoDBSteps.VerificaProjetoExiste(nomeProjeto));
            });
        }

        [Test]
        public void NomeProjetoNaoInformado()
        {
            #region Parameters 
            string descricaoErro = "call to undefined method restfault::getmessage() in ";
            string statusCodeEsperado = "OK";
            #endregion
            cadastraProjetoRequests.SetJsonBody("", "");
            IRestResponse<dynamic> response = cadastraProjetoRequests.ExecuteRequest();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.That(true, descricaoErro, response.Content);
            });
        }
    }
}