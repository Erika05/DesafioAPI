using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class DeletaTagCopiaTarefasTests : TestBase
    {
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();
        CadastroTarefaRequest cadastroTarefaRequest = new CadastroTarefaRequest();
        AdicionaTagCopiaTarefaRequest adicionaTagCopiaTarefaRequest = new AdicionaTagCopiaTarefaRequest();
        DeletaTagCopiaTarefaRequest deletaTagCopiaTarefaRequest = new DeletaTagCopiaTarefaRequest();

        [Test]
        public void DeletarTagCopiaTarefa()
        {
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa tag tarefas";
            string descricao = "Descricao tag tarefa";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion           
            string statusCodeEsperado = "OK";
            VerificaProjetoExiste(projeto);
            cadastroTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            string idTarefaRelacionada = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["id"];

            adicionaTagCopiaTarefaRequest.SetParameters(idTarefa);
            adicionaTagCopiaTarefaRequest.SetJsonBody(idTarefaRelacionada);
            IRestResponse<dynamic> responseAddCopiaTarefa = adicionaTagCopiaTarefaRequest.ExecuteRequest();

            string retornoIdTarefaRelacionada = responseAddCopiaTarefa.Data["issue"]["relationships"][0]["id"];
            
            deletaTagCopiaTarefaRequest.SetParameters(idTarefa, retornoIdTarefaRelacionada);
            IRestResponse<dynamic> response = deletaTagCopiaTarefaRequest.ExecuteRequest();


            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(0, TarefaDBSteps.VerificaCopiaTarefaExiste(retornoIdTarefaRelacionada));
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