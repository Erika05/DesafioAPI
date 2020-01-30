using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Projeto
{
    [TestFixture]
    public class CadastroProjetoTests : TestBase
    {
        CadastroProjetoRequests cadastroProjetoRequests = new CadastroProjetoRequests();
        DeleteProjetoRequests deleteProjetoRequests = new DeleteProjetoRequests();

        [Test]
        public void CadastrarProjetoSucesso()
        {
            #region Parameters 
            string name = "perojet teste";
            string descricao = "projeto teste";
            string statusCodeEsperado = "Created";
            #endregion
            VerificaProjetoExiste(name);
            cadastroProjetoRequests.SetJsonBody(name, descricao);
            IRestResponse<dynamic> response = cadastroProjetoRequests.ExecuteRequest();
            string retornoNomeProjetc = response.Data["project"]["name"];
            string retornoDescricaoProjetc = response.Data["project"]["description"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(name, retornoNomeProjetc);
                Assert.AreEqual(descricao, retornoDescricaoProjetc);
            });
        }

        //[Test]
        //public void ProjetoJaCadastrado()
        //{
        //    #region Parameters 
        //    string name = "projeto geral 1";
        //    string descricao = "Call to undefined method RestFault::getMessage() in ";
        //    string statusCodeEsperado = "OK";
        //    #endregion
        //    cadastroProjetoRequests.SetJsonBody(name, descricao);
        //    IRestResponse<dynamic> response = cadastroProjetoRequests.ExecuteRequest();
        //    Assert.Multiple(() =>
        //    {
        //        Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());

        //    });
        //}
        public void VerificaProjetoExiste(string nomeProjeto)
        {
            if (ProjetoDBSteps.VerificaProjetoExiste(nomeProjeto).Equals(1))
            {
                deleteProjetoRequests.SetParameters(ProjetoDBSteps.RetornaIDProjetoNome(nomeProjeto));
                deleteProjetoRequests.ExecuteRequest();
            }
        }
    }
}