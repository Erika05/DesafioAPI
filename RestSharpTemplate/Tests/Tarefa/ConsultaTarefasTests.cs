using DesafioAPI.Bases;
using NUnit.Framework;
using RestSharp;
using DesafioAPI.Requests.Tarefas;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;

namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class ConsultaTarefasTests : TestBase
    {
        ConsultaTodasTarefasRequest consultaTodasTarefasRequest = new ConsultaTodasTarefasRequest();
        ConsultaTarefasRequest consultaTarefasRequest = new ConsultaTarefasRequest();
        CadastroTarefaRequest cadastroTarefaRequest = new CadastroTarefaRequest();
        CadastroProjetoRequests cadastroProjetoRequests = new CadastroProjetoRequests();
        ConsultaTarefasAnexosRequest consultaTarefasAnexosRequest = new ConsultaTarefasAnexosRequest();

        [Test]
        public void ConsultarTarefa()
        {           
            #region Parameters
            string resumo = "Tarefa consulta";
            string descricao = "descricao";
            string categoria = "General";
            string projeto = "projeto geral";            
            string statusCodeEsperado = "OK";
            VerificaProjetoExiste(projeto);
            cadastroTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            #endregion
            consultaTarefasRequest.SetParameters(idTarefa);
            IRestResponse<dynamic> response = consultaTarefasRequest.ExecuteRequest();
            string retornoId = response.Data["issues"][0]["id"];
            string retornoSummary = response.Data["issues"][0]["summary"];
            string retornoDescription = response.Data["issues"][0]["description"];
            string retornoProject = response.Data["issues"][0]["project"]["name"];
            string retornoCategory = response.Data["issues"][0]["category"]["name"];

            Assert.Multiple(() =>
                {
                    Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                    Assert.AreEqual(idTarefa, retornoId);
                    Assert.AreEqual(resumo, retornoSummary);
                    Assert.AreEqual(descricao, retornoDescription);
                    Assert.AreEqual(projeto, retornoProject);
                    Assert.AreEqual(categoria, retornoCategory);
                });
        }

        [Test]
        public void ConsultarTodasTarefas()
        {
            #region Parameters
            string statusCodeEsperado = "OK";
            #endregion            
            consultaTodasTarefasRequest.SetParameters("10", "1");
            IRestResponse<dynamic> response = consultaTodasTarefasRequest.ExecuteRequest();
            Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
        }

        [Test]
        public void ConsultarTarefaArquivo()
        {
            #region Parameters
            string resumo = "Tarefa consulta";
            string descricao = "descricao";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusCodeEsperado = "OK";
            string nomeAnexo = "test.txt";
            string anexo = "VGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4NClRoaXMgaXMgYSBURVNULg0KVGhpcyBpcyBhIFRFU1QuDQpUaGlzIGlzIGEgVEVTVC4=";
            VerificaProjetoExiste(projeto);
            cadastroTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto, nomeAnexo, anexo);
            string idTarefa = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            string idAnexo = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["attachments"][0]["id"];
            #endregion
            consultaTarefasAnexosRequest.SetParameters(idTarefa, idAnexo);
            IRestResponse<dynamic> response = consultaTarefasAnexosRequest.ExecuteRequest();
            string retornoId = response.Data["files"][0]["id"];
            string retornoNomeAnexo = response.Data["files"][0]["filename"];
            string retornoAnexo = response.Data["files"][0]["content"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(idTarefa, retornoId);
                Assert.AreEqual(nomeAnexo, retornoAnexo);
                Assert.AreEqual(anexo, retornoAnexo);
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
