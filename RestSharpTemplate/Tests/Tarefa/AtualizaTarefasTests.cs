using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefa;
using DesafioAPI.Requests.Tarefas;
using NUnit.Framework;
using RestSharp;


namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class AtualizaTarefasTests : TestBase
    {
        CadastroProjetoRequests cadastroProjetoRequests = new CadastroProjetoRequests();
        CadastroTarefaRequest cadastroTarefaRequest = new CadastroTarefaRequest();
        AtualizaTarefaRequest atualizaTarefaRequest = new AtualizaTarefaRequest();

        [Test]
        public void AtualizarTarefaMinimal()
        {
            #region Parameters
            string resumo = "This is a test issue";
            string descricao = "This is a test description";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusTarefa = "resolved";
            string statusCodeEsperado = "OK";            
            #endregion
            VerificaProjetoExiste(projeto);
            cadastroTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            atualizaTarefaRequest.SetParameters(idTarefa);
            atualizaTarefaRequest.SetJsonBody(statusTarefa);
            IRestResponse<dynamic> response = atualizaTarefaRequest.ExecuteRequest();

            string retornoStatusTarefa = response.Data["issues"][0]["status"]["name"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(statusTarefa, retornoStatusTarefa);
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