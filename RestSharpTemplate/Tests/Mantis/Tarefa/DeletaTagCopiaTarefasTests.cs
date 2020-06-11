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
    public class DeletaTagCopiaTarefasTests : TestBase
    {
        HelpersProjetos helpersProjetos = new HelpersProjetos();
        CadastraTarefaRequest cadastraTarefaRequest = new CadastraTarefaRequest();
        AdicionaTagCopiaTarefaRequest adicionaTagCopiaTarefaRequest = new AdicionaTagCopiaTarefaRequest();
        DeletaTagCopiaTarefaRequest deletaTagCopiaTarefaRequest = new DeletaTagCopiaTarefaRequest();

        [Test]
        public void DeletarTagCopiaTarefa()
        {
            #region Parameters Cadastro Tarefa
            string resumo = "Deletar copia tag tarefas";
            string descricao = "Descricao copia tag tarefa";
            string projeto = "projeto geral";
            string categoria = "General";
            #endregion           
            string statusCodeEsperado = "OK";
            helpersProjetos.PreparaBaseCadastradoProjeto(projeto);
            cadastraTarefaRequest.SetJsonBody(resumo, descricao, categoria, projeto);
            string idTarefa = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];
            string idTarefaRelacionada = cadastraTarefaRequest.ExecuteRequest().Data["issue"]["id"];

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
      
    }
}