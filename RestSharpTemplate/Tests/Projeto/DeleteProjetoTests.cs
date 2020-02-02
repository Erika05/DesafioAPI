using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAPI.Tests.Projeto
{
    [TestFixture]
    public class DeleteProjetoTests : TestBase
    {
        DeleteProjetoRequests deleteProjetoRequests = new DeleteProjetoRequests();
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();

        [Test]
        public void DeletarProjeto()
        {
            #region Parameters
            string nomeProjeto = "projeto delete";
            string statusCodeEsperado = "OK";
            #endregion
            VerificaProjetoExiste(nomeProjeto);
            deleteProjetoRequests.SetParameters(ProjetoDBSteps.RetornaIDProjetoNome(nomeProjeto));
            IRestResponse<dynamic> response = deleteProjetoRequests.ExecuteRequest();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
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
