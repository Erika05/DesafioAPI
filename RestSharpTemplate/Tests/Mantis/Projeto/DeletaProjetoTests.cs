using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Projeto
{
    [TestFixture]
    public class DeletaProjetoTests : TestBase
    {
        DeletaProjetoRequests deletaProjetoRequests = new DeletaProjetoRequests();
        HelpersProjetos helpersProjetos = new HelpersProjetos();

        [Test]
        public void DeletarProjeto()
        {
            #region Parameters
            string nomeProjeto = "projeto delete";
            string statusCodeEsperado = "OK";
            #endregion
            string idProjeto = helpersProjetos.PreparaBaseCadastradoProjeto(nomeProjeto);
            deletaProjetoRequests.SetParameters(idProjeto);
            IRestResponse<dynamic> response = deletaProjetoRequests.ExecuteRequest();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(0, ProjetoDBSteps.VerificaProjetoExiste(nomeProjeto));               
            });
        }
    }
}
