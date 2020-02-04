using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Tarefas
{
    [TestFixture]
    public class DeletaNotaTarefasTests : TestBase
    {
        CadastraProjetoRequests cadastroProjetoRequests = new CadastraProjetoRequests();
        CadastroTarefaRequest cadastroTarefaRequest = new CadastroTarefaRequest();
        AdicionaNotaTarefaRequest adicionaNotaTarefaRequest = new AdicionaNotaTarefaRequest();
        DeletaNotaTarefaRequest deletaNotaTarefaRequest = new DeletaNotaTarefaRequest();

        [Test]
        public void DeletarNotaTarefa()
        {
            //Criar tag 
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa deletar nota";
            string descricao = "Descricao deletar nota tarefa";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            #region Adiciona nota Tarefa
            string nota = "nota delete";
            string statusNota = "private";
            string statusCodeEsperado = "OK";
            #endregion
            cadastroTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastroTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            adicionaNotaTarefaRequest.SetParameters(idTarefa);
            adicionaNotaTarefaRequest.SetJsonBody(nota, statusNota);
            string idNota = adicionaNotaTarefaRequest.ExecuteRequest().Data["note"]["id"];

            deletaNotaTarefaRequest.SetParameters(idTarefa, idNota);
            deletaNotaTarefaRequest.SetJsonBody(nota, statusNota);

            IRestResponse<dynamic> response = deletaNotaTarefaRequest.ExecuteRequest();
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