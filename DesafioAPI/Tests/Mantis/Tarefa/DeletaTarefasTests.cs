using DesafioAPI.Bases;
using DesafioAPI.DBSteps;
using DesafioAPI.Requests.Projeto;
using DesafioAPI.Requests.Tarefa;
using DesafioAPI.Requests.Tarefas;
using DesafioAPI.Tests.Mantis.Projeto;
using NUnit.Framework;
using RestSharp;
using System.Threading;

namespace DesafioAPI.Tests.Mantis.Tarefas
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class DeletaTarefasTests : TestBase
    {
        HelpersProjetos helpersProjetos = new HelpersProjetos();
        CadastraTarefaRequest cadastraTarefaRequest = new CadastraTarefaRequest();       
        DeletaTarefaRequest deletaTarefaRequest = new DeletaTarefaRequest();

       [Test]
        public void DeletarTarefa()
        {
            #region Parameters Cadastro Tarefa
            string resumo = "Tarefa Delete";
            string descricao = "Descricao tarefa delete";
            string categoria = "General";
            string projeto = "projeto geral";
            string statusCodeEsperado = "NoContent";
            #endregion
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            deletaTarefaRequest.SetParameters(idTarefa);

            IRestResponse<dynamic> response = deletaTarefaRequest.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.AreEqual(0, TarefaDBSteps.VerificaTarefaExiste(idTarefa));

            });
        }

        [Test]
        public void DeletarTarefaNaoEncontrada()
        {
            #region Parametros
            string tarefaInexistente = "78978678988";
            string statusCodeEsperado = "NotFound";
            string descricaoErro = "not foundd";
            #endregion
            deletaTarefaRequest.SetParameters(tarefaInexistente);
            IRestResponse<dynamic> response = deletaTarefaRequest.ExecuteRequest();
            string retornoMensagemErro = response.Data["message"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeEsperado, response.StatusCode.ToString());
                Assert.That(true, descricaoErro, retornoMensagemErro);
            });
        }
    }
}