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
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();
        AtualizaProjetoRequests atualizaProjetoRequests = new AtualizaProjetoRequests();
        DeletaProjetoRequests deleteProjetoRequests = new DeletaProjetoRequests();

        [Test]
        public void AtualizarProjeto()
        {
            #region Parameters 
            string nomeProjetoEdicao = "projeto atualizar";
            string statusCodeEsperado = "OK";
            string nomeProjetoNovo = "projeto novo";
            #endregion
            VerificaProjetoExiste(nomeProjetoEdicao, nomeProjetoNovo);
            string idProjeto = ProjetoDBSteps.RetornaIDProjetoNome(nomeProjetoEdicao);
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
            });
        }

        public void VerificaProjetoExiste(string nomeProjetoEdicao, string nomeProjetoNovo)
        {
            if (ProjetoDBSteps.VerificaProjetoExiste(nomeProjetoEdicao).Equals(0))
            {
                cadastroProjetoRequests.SetJsonBody(nomeProjetoEdicao, "desc");
                cadastroProjetoRequests.ExecuteRequest();
            }

            if (ProjetoDBSteps.VerificaProjetoExiste(nomeProjetoNovo).Equals(1))
            {
                deleteProjetoRequests.SetParameters(ProjetoDBSteps.RetornaIDProjetoNome(nomeProjetoNovo));
                deleteProjetoRequests.ExecuteRequest();
            }
        }
    }
}