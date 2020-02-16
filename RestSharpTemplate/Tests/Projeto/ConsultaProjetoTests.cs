using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Projeto
{

    [TestFixture]
    public class ConsultaProjetoTests : TestBase
    {
        CadastraProjetoRequests cadastraProjetoRequests = new CadastraProjetoRequests();
        ConsultaProjetoRequests consultaProjetoRequests = new ConsultaProjetoRequests();
        ConsultaTodosProjetosRequest consultaTodosProjetosRequest = new ConsultaTodosProjetosRequest();

        [Test]
        public void ConsultarProjeto()
        {
            #region Parameters
            string nomeProjeto = "projeto consulta";
            string statusCodeEsperado = "OK";
            #endregion
            cadastraProjetoRequests.SetJsonBody(nomeProjeto, "descricao");
            string idProjeto = cadastraProjetoRequests.ExecuteRequest().Data["project"]["id"];

            consultaProjetoRequests.SetParameters(idProjeto);
            IRestResponse<dynamic> response = consultaProjetoRequests.ExecuteRequest();
            string retornoIdProjetc = response.Data["projects"][0]["id"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(idProjeto, retornoIdProjetc);
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
            });
        }

        [Test]
        public void ConsultarTodosProjetos()
        {
            #region Parameters
            string nomeProjeto = "projeto consulta";
            string statusCodeEsperado = "OK";
            #endregion
            cadastraProjetoRequests.SetJsonBody(nomeProjeto, "descricao");
            cadastraProjetoRequests.ExecuteRequest();
            IRestResponse<dynamic> response = consultaTodosProjetosRequest.ExecuteRequest();
            Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
        }
    }
}