using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Projeto
{
    [TestFixture]
    public class AtualizaProjetoTests : TestBase
    {
        AtualizaProjetoRequests atualizaProjetoRequests = new AtualizaProjetoRequests();
        HelpersProjetos helpersProjetos = new HelpersProjetos();

        [Test]
        public void AtualizarProjeto()
        {
            #region Parameters 
            string nomeProjetoEdicao = "projeto atualizar";
            string statusCodeEsperado = "OK";
            string nomeProjetoNovo = "projeto novo";
            #endregion
           string idProjeto = helpersProjetos.PreparaBaseCadastradoProjeto(nomeProjetoEdicao);
            helpersProjetos.PreparaBaseDeletadoProjeto(nomeProjetoNovo);
            atualizaProjetoRequests.SetParameters(idProjeto);
            atualizaProjetoRequests.SetJsonBody(nomeProjetoNovo, idProjeto);
            IRestResponse<dynamic> response = atualizaProjetoRequests.ExecuteRequest();
            string retornoNomeProjetc = response.Data["project"]["name"];
            string retornoIdProjetc = response.Data["project"]["id"];
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(nomeProjetoNovo, retornoNomeProjetc);
                Assert.AreEqual(idProjeto, retornoIdProjetc);
                Assert.AreEqual(0, ProjetoDBSteps.VerificaProjetoExiste(nomeProjetoEdicao));
                Assert.AreEqual(1, ProjetoDBSteps.VerificaProjetoExiste(nomeProjetoNovo));
            });
        }
    }
}