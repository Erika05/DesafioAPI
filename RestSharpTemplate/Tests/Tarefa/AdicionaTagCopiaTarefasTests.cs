using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class AdicionaTagCopiaTarefasTests : TestBase
    {
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();
        CadastroTarefaRequest cadastroTarefaRequest = new CadastroTarefaRequest();
        AdicionaTagCopiaTarefaRequest adicionaTagCopiaTarefaRequest = new AdicionaTagCopiaTarefaRequest();

        [Test]
        public void AdicionarTagCopiaTarefa()
        {
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa tag tarefas";
            string descricao = "Descricao tag tarefa";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion           
            string statusCodeEsperado = "Created";
            cadastroTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            string idRarefaRelacionada = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["id"];

            adicionaTagCopiaTarefaRequest.SetParameters(idTarefa);
            adicionaTagCopiaTarefaRequest.SetJsonBody(idRarefaRelacionada);
            IRestResponse<dynamic> response = adicionaTagCopiaTarefaRequest.ExecuteRequest();

           string retornoIdTarefaRelacionada = response.Data["issue"]["relationships"][0]["issue"]["id"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(idRarefaRelacionada, retornoIdTarefaRelacionada);
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