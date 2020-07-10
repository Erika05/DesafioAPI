using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefas;
using DesafioAPI.Tests.Mantis.Projeto;
using NUnit.Framework;
using RestSharp;

namespace DesafioAPI.Tests.Mantis.Tarefas
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class DeletaNotaTarefasTests : TestBase
    {
        HelpersProjetos helpersProjetos = new HelpersProjetos();
        CadastraTarefaRequest cadastraTarefaRequest = new CadastraTarefaRequest();
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
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            adicionaNotaTarefaRequest.SetParameters(idTarefa);
            adicionaNotaTarefaRequest.SetJsonBody(nota, statusNota);
            string idNota = adicionaNotaTarefaRequest.ExecuteRequest().Data["note"]["id"];

            deletaNotaTarefaRequest.SetParameters(idTarefa, idNota);
            deletaNotaTarefaRequest.SetJsonBody(nota, statusNota);

            IRestResponse<dynamic> response = deletaNotaTarefaRequest.ExecuteRequest();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(0, TarefaDBSteps.VerificaNotaTarefaExiste(idNota));
            });
        }

        [Test]
        public void NotaTarefaNaoEncontrada()
        {
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa deletar nota teste teste";
            string descricao = "Descricao deletar nota tarefa testes";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion
            #region Adiciona nota Tarefa
            string notaInexistente = "78978678988";
            string statusCodeEsperado = "NotFound";
            string descricaoErro = "not foundd";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            adicionaNotaTarefaRequest.SetParameters(idTarefa);
            deletaNotaTarefaRequest.SetParameters(idTarefa, notaInexistente);
            deletaNotaTarefaRequest.SetJsonBody(notaInexistente, "");

            IRestResponse<dynamic> response = deletaNotaTarefaRequest.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.That(true, descricaoErro, retornoMensagemErro);
            });
        }
    }
}