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
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();
        ConsultaProjetoRequests consultaProjetoRequests = new ConsultaProjetoRequests();
        ConsultaTodosProjetosRequest consultaTodosProjetosRequest = new ConsultaTodosProjetosRequest();

        [Test]
        public void ConsultarProjeto()
        {
            #region Parameters
            string idProjeto = ProjetoDBSteps.RetornaIDProjeto();
            string statusCodeEsperado = "OK";
            #endregion
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
            string statusCodeEsperado = "OK";
            #endregion            
            IRestResponse<dynamic> response = consultaTodosProjetosRequest.ExecuteRequest();
            Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
        }
    }
}